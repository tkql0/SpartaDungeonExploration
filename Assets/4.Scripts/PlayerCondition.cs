using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageIbe
{
    void TakePhysicalDamage(int inDamage);
}


public class PlayerCondition : MonoBehaviour, IDamageIbe
{
    public UICondition uICondition;

    Condition health { get { return uICondition.health; } }
    // �Լ� ������

    public event Action OnTakeDamage;
    // �̺�Ʈ ������ �𸣰ھ�

    public void Heal(float inAmout)
    {
        health.Add(inAmout);
        // ü���� ä���
    }

    public void TakePhysicalDamage(int inDamage)
    {
        health.Subtract(inDamage);
        // �������� ����
        OnTakeDamage?.Invoke();
        // ȭ�� ������ �����̱�
    }
}
