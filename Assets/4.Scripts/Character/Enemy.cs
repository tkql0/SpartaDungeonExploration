using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Monster,
    Trap,
    JumpPad,
}

public class Enemy : MonoBehaviour
{ // Enemy�� ������ó�� Type�� ���������� ���ڴ�
    [Header("Combat")]
    public int damage;
    public float damageRate;

    public EnemyType enemyType;

    List<IDamageIbe> things = new List<IDamageIbe>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    public virtual void Attack()
    {

    }

    private void OnTriggerEnter(Collider inOther)
    {
        if (inOther.TryGetComponent<IDamageIbe>(out var outDamageIbe))
        {
            things.Add(outDamageIbe);
        }
    }

    private void OnTriggerExit(Collider inOther)
    {
        if (inOther.TryGetComponent<IDamageIbe>(out var outDamageIbe))
        {
            things.Remove(outDamageIbe);
        }
    }
}
