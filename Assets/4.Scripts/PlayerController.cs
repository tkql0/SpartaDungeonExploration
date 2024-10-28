using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 _inputVector;
    public LayerMask groundLayerMask;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(_inputVector.x, 0, _inputVector.y).normalized
             * moveSpeed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + dir);
    }

    public void OnMove(InputAction.CallbackContext inContext)
    { // InputSystem���� �� �޾ƿ���
        if (inContext.phase == InputActionPhase.Started)
        { // ���� Ű���带 ���ȴٸ�
            _inputVector = inContext.ReadValue<Vector2>();
            // Vector2 �� �޾ƿ���
            _animator.SetBool("isMove", true);
        }
        else if (inContext.phase == InputActionPhase.Canceled)
        { // Ű���带 ���ٸ�
            _inputVector = Vector2.zero;
            // _inputVector �� �ʱ�ȭ
            _animator.SetBool("isMove", false);
        }
    }
}
