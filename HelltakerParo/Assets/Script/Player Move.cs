using UnityEngine;
using UnityEngine.InputSystem; // �VInput System�p

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f; // �ړ����x

    [SerializeField]
    private Vector2 moveInput;   // ���͂��ꂽ�����x�N�g��

    void Update()
    {
        // ���͕����Ɉړ��i�t���[�����ƂɍX�V�j
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("a");
        moveInput = value.Get<Vector2>();
    }
}
