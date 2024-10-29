using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemDate;
    // 플레이어가 바라보는 아이템이었나?
    // 이부분 다시 봐야겠다. // Interaction이랑 ItemObject에서 처리하는 거구나
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        // 체력은 UI인데 여기서 초기화하는 이유도 모르겠어
    }
}
