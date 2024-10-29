using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    // ��ũ��Ʈ �ȿ��� �Ҵ��غ���
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3
                (Screen.width / 2, Screen.height / 2));
            // ȭ�� ������ ��, ȭ�� ������ �� �̸� ���߾��̱���
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            { // maxCheckDistance���� Ž���ؼ� ���� ����� ������ Ž��
                if (hit.collider.gameObject != curInteractGameObject)
                { // ���� Ž���� ������Ʈ�� ���°�
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    // �����ɽ�Ʈ�� ���� ������ curInteractable�� �޾ƿ���
                    SetPromptText();
                }
            }
            else
            { // Ž���� ������Ʈ�� ���ٸ�
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
                // ���� ��Ȱ��ȭ
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        // ������ ������ ���� ������Ʈ Ȱ��ȭ
        promptText.text = curInteractable.GetInteractPrompt();
        // ���� ���
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        { // Ư�� Ű�� �����ٸ� ������ ���� ȭ���� �����ִٸ�
            curInteractable.OnInteract();
            // ������ ������ �÷��̾�� ������
            curInteractGameObject = null;
            curInteractable = null;
            // ������ ���� �ʱ�ȭ
            promptText.gameObject.SetActive(false);
            // ���� ��Ȱ��ȭ
        }
    }
}
