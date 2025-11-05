using UnityEngine;

public class PlayerGoalChecker : MonoBehaviour
{
    [Header("ゴール")]
    [SerializeField] private Transform goalTransform;

    [Header("判定の許容誤差")]
    [SerializeField] private float threshold = 0.01f;

    private bool goalReached = false;

    void Update()
    {
        if (goalReached) return;

        float distance = Vector2.Distance(transform.position, goalTransform.position);
        if (distance < threshold)
        {
            goalReached = true;
            Debug.Log("プレイヤーがゴールに到達！クリア！");
            // ここで演出やステージクリア処理を呼び出す
        }
    }
}
