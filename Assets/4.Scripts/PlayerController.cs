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

    [Header("Look")]
    public Transform cameraContainer;
    public float minLook;
    public float maxLook;
    private float camCurXPot;
    public float lookSensitivity; // 민감도
    private Vector2 mouseDelta;
    public bool canLock = true;

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

    private void LateUpdate()
    {
        if (canLock)
        {
            Look();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * _inputVector.y +
            transform.right * _inputVector.x;

        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        // 점프외에는 현재 상태 0

        _rigidbody.velocity = dir;
    }

    void Look()
    {
        camCurXPot += mouseDelta.y * lookSensitivity;
        //돌려줄 값을 마우스 델타.y에서 뽑아오고
        //x방향을 돌리기 위해선 y축을 돌리고 y방향을 돌릴땐 반대로 x축을 돌리고
        camCurXPot = Mathf.Clamp(camCurXPot, minLook, maxLook);
        // 최소값, 최대값을 넘지않게 Mathf.Clamp 사용
        cameraContainer.localEulerAngles = new Vector3(-camCurXPot, 0, 0);
        // -를 해야 위를 바라보고 +를 해야 아래를 보기 때문에 
        // camCurXPot을 -로 설정

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
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

    public void OnLook(InputAction.CallbackContext inContext)
    { // InputSystem에서 값 받아오기
        mouseDelta = inContext.ReadValue<Vector2>();
        // 마우스 Vector2 받아오기
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLock = !toggle;
    }
}
