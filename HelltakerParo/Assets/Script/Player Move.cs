using UnityEngine;
using UnityEngine.InputSystem; // �VInput System�p

public class PlayerMove : MonoBehaviour
{
    [Header("1�}�X�ړ�")]
    [SerializeField]
    [Range(1f,10f)]
    //1��̈ړ�����(1�}�X)
    private float moveDistance = 1f; 

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Debug.Log("Move input: " + input);

        // ���͕������[������Ȃ��Ƃ���������
        if (input != Vector2.zero)
        {
            Vector3 moveDir = new Vector3(input.x, input.y, 0f).normalized;
            transform.position += moveDir * moveDistance;
        }
    }

}
