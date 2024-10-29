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
    public float lookSensitivity; // �ΰ���
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
        //������ ���� ���콺 ��Ÿ.y���� �̾ƿ���
        //x������ ������ ���ؼ� y���� ������ y������ ������ �ݴ�� x���� ������
        camCurXPot = Mathf.Clamp(camCurXPot, minXLook, maxXLook);
        // �ּҰ�, �ִ밪�� �����ʰ� Mathf.Clamp ���
        cameraContainer.localEulerAngles = new Vector3(-camCurXPot, 0, 0);
        // -�� �ؾ� ���� �ٶ󺸰� +�� �ؾ� �Ʒ��� ���� ������ 
        // camCurXPot�� -�� ����

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
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

    public void OnLook(InputAction.CallbackContext inContext)
    { // InputSystem���� �� �޾ƿ���
        mouseDelta = inContext.ReadValue<Vector2>();
        // ���콺 Vector2 �޾ƿ���
    }

    public void OnJump(InputAction.CallbackContext inContext)
    {
        if (inContext.phase == InputActionPhase.Started && IsJump())
        { // Ű���带 ������ �� ���� ������ ���¶��
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            // ���� �߻�
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
