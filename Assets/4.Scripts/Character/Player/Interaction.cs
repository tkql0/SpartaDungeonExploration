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
    // 스크립트 안에서 할당해보기
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
            // 화면 가로의 반, 화면 세로의 반 이면 정중앙이구나
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            { // maxCheckDistance까지 탐색해서 가장 가까운 아이템 탐색
                if (hit.collider.gameObject != curInteractGameObject)
                { // 이전 탐색된 오브젝트가 없는가
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    // 레이케스트에 닿은 아이템 curInteractable에 받아오기
                    SetPromptText();
                }
            }
            else
            { // 탐색된 오브젝트가 없다면
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
                // 설명 비활성화
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        // 아이템 설명이 나올 오브젝트 활성화
        promptText.text = curInteractable.GetInteractPrompt();
        // 설명 출력
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        { // 특정 키를 눌렀다면 아이템 설명 화면이 켜져있다면
            curInteractable.OnInteract();
            // 아이템 정보를 플레이어로 보내기
            curInteractGameObject = null;
            curInteractable = null;
            // 아이템 정보 초기화
            promptText.gameObject.SetActive(false);
            // 설명 비활성화
        }
    }
}
