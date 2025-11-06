using UnityEngine;
using UnityEngine.InputSystem; // 新Input System用

public class PlayerMove : MonoBehaviour
{
    [Header("1マス移動")]
    [SerializeField]
    [Range(1f,10f)]
    //1回の移動距離(1マス)
    private float moveDistance = 1f;

    [Header("壁")]
    [SerializeField]
    private string wallTag = "Wall";

    [Header("木箱")]
    [SerializeField]
    private string boxTag = "Box";

    [Header("敵")]
    [SerializeField]
    private string enemyTag = "Enemy";


   public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input == Vector2.zero) return;

        Vector3 moveDir = new Vector3(input.x, input.y, 0f).normalized;
        Vector3 targetPos = transform.position + moveDir * moveDistance;

        // 移動先に何かあるか調べる
        Collider2D hit = Physics2D.OverlapPoint(targetPos);

        if (hit == null)
        {
            // 何もなければ移動
            transform.position = targetPos;
        }
        else if (hit.CompareTag(boxTag))
        {
            // 木箱なら、さらにその先をチェック
            Vector3 boxTarget = hit.transform.position + moveDir * moveDistance;
            Collider2D boxHit = Physics2D.OverlapPoint(boxTarget);

            if (boxHit == null || !boxHit.CompareTag(wallTag) && !boxHit.CompareTag(boxTag))
            {
                // 木箱を押す（壁がなければ）
                hit.transform.position = boxTarget;
                transform.position = targetPos;
            }
            else
            {
                Debug.Log("木箱の先になにかあるので押せない");
            }

        }
        else if (hit.CompareTag(enemyTag))
        {
            Vector3 enemyTarget = hit.transform.position + moveDir * moveDistance;
            Collider2D enemyHit = Physics2D.OverlapPoint(enemyTarget);

            if (enemyHit != null && enemyHit.CompareTag(wallTag))
            {
                // 壁がある → エネミーを消滅させる
                Destroy(hit.gameObject);
                Debug.Log("エネミーが壁に押し出されて消滅！");
                transform.position = targetPos;
                
            }
            else if (enemyHit == null || !enemyHit.CompareTag(boxTag) && !enemyHit.CompareTag(enemyTag))
            {
                // 壁も箱もエネミーもない → 押せる
                hit.transform.position = enemyTarget;
                transform.position = targetPos;
                
            }
            else
            {
                Debug.Log("エネミーの先に障害物があるので押せない");
            }
        }

        else if (hit.CompareTag(wallTag))
        {
            Debug.Log("壁があるので進めない");
        }
    }
}
