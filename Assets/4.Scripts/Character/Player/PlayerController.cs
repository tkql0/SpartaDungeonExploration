using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public float minXLook;
    public float maxXLook;
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

    private void Update()
    {
        Debug.DrawRay(transform.position + (transform.up * 0.01f), Vector3.down * 1f, Color.red);
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
        Vector3 dir = (transform.forward * _inputVector.y +
            transform.right * _inputVector.x) * moveSpeed;

        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void Look()
    {
        camCurXPot += mouseDelta.y * lookSensitivity;
        //돌려줄 값을 마우스 델타.y에서 뽑아오고
        //x방향을 돌리기 위해선 y축을 돌리고 y방향을 돌릴땐 반대로 x축을 돌리고
        camCurXPot = Mathf.Clamp(camCurXPot, minXLook, maxXLook);
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

    public void OnJump(InputAction.CallbackContext inContext)
    {
        if (inContext.phase == InputActionPhase.Started && IsJump())
        { // 키보드를 눌렀을 때 점프 가능한 상태라면
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            // 위로 발사
        }
    }

    bool IsJump()
    {
        Ray ray = new Ray(transform.position + (transform.up * 0.01f), Vector3.down);

        if (Physics.Raycast(ray, 1f, groundLayerMask))
        {
            return true;
        }

        return false;
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLock = !toggle;
    }

    private void OnCollisionEnter(Collision inCollision)
    {
        if(inCollision.gameObject.TryGetComponent<Enemy>(out var outEnemy))
        {
            if(outEnemy.enemyType == EnemyType.JumpPad)
            {
                _rigidbody.AddForce(Vector2.up * jumpPower * 2, ForceMode.Impulse);
            }
        }
    }
}
