using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    // 플레이어가 바라보는 아이템이었나?
    // 이부분 다시 봐야겠다. // Interaction이랑 ItemObject에서 처리하는 거구나
    public Action addItem;

    public Transform dropPosition;
    // 버리기 선택시 아이템이 생성될 위치

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
