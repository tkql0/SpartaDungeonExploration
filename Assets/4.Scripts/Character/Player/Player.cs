using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    // �÷��̾ �ٶ󺸴� �������̾���?
    // �̺κ� �ٽ� ���߰ڴ�. // Interaction�̶� ItemObject���� ó���ϴ� �ű���
    public Action addItem;

    public Transform dropPosition;
    // ������ ���ý� �������� ������ ��ġ

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
