using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ // Enemy�� ������ó�� Type�� ���������� ���ڴ�
    [Header("Combat")]
    public int damage;
    public float damageRate;

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
