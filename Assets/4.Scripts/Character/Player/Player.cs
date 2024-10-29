using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemDate;
    // �÷��̾ �ٶ󺸴� �������̾���?
    // �̺κ� �ٽ� ���߰ڴ�. // Interaction�̶� ItemObject���� ó���ϴ� �ű���
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        // ü���� UI�ε� ���⼭ �ʱ�ȭ�ϴ� ������ �𸣰ھ�
    }
}
