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
    // 함수 순서랑

    public event Action OnTakeDamage;
    // 이벤트 순서를 모르겠어

    public void Heal(float inAmout)
    {
        health.Add(inAmout);
        // 체력을 채우고
    }

    public void TakePhysicalDamage(int inDamage)
    {
        health.Subtract(inDamage);
        // 데미지를 빼고
        OnTakeDamage?.Invoke();
        // 화면 빨갛게 물들이기
    }
}
