using UnityEngine;
using UnityEngine.InputSystem; // 新Input System用

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f; // 移動速度

    [SerializeField]
    private Vector2 moveInput;   // 入力された方向ベクトル

    void Update()
    {
        // 入力方向に移動（フレームごとに更新）
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("a");
        moveInput = value.Get<Vector2>();
    }
}
