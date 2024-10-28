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
    { // InputSystem에서 값 받아오기
        if (inContext.phase == InputActionPhase.Started)
        { // 만약 키보드를 눌렸다면
            _inputVector = inContext.ReadValue<Vector2>();
            // Vector2 값 받아오기
            _animator.SetBool("isMove", true);
        }
        else if (inContext.phase == InputActionPhase.Canceled)
        { // 키보드를 땠다면
            _inputVector = Vector2.zero;
            // _inputVector 값 초기화
            _animator.SetBool("isMove", false);
        }
    }
}
