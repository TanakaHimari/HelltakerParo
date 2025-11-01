using UnityEngine;
using UnityEngine.InputSystem; // 新Input System用

public class PlayerMove : MonoBehaviour
{
    [Header("1マス移動")]
    [SerializeField]
    [Range(1f,10f)]
    //1回の移動距離(1マス)
    private float moveDistance = 1f; 

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Debug.Log("Move input: " + input);

        // 入力方向がゼロじゃないときだけ動く
        if (input != Vector2.zero)
        {
            Vector3 moveDir = new Vector3(input.x, input.y, 0f).normalized;
            transform.position += moveDir * moveDistance;
        }
    }

}
