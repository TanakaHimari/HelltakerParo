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

   public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input == Vector2.zero) return;

        Vector3 moveDir = new Vector3(input.x, input.y, 0f).normalized;
        Vector3 targetPos = transform.position + moveDir * moveDistance;

        // 移動先に壁タグのオブジェクトがあるか判定
        Collider2D hit = Physics2D.OverlapPoint(targetPos);
        if (hit != null && hit.CompareTag(wallTag))
        {
            Debug.Log("壁があるので移動しない");
            return;
        }

        transform.position = targetPos;
    }
}
