using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;

    public Image uiBar;

    private void Start()
    {
        curValue = maxValue;
    }

    private void Update()
    {
        // ui ������Ʈ
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    { // ���� ü�� ������Ʈ
        return curValue / maxValue;
        // �� �̺�Ʈ�� ���� �� �����Ű����� �� �ȵǳ�
        // �ʼ� ���� ������ �ٽ� �غ��߰ڴ�
    }

    public void Add(float inValue)
    { // ȣ��Ǹ� �ش� �� ä���
        curValue = Mathf.Min(curValue + inValue, maxValue);
    }

    public void Subtract(float inValue)
    { // ȣ��Ǹ� �ش� �� ����
        curValue = Mathf.Max(curValue - inValue, 0.0f);
    }
    // �ð� ���� ���� �� �̺�Ʈ�� ����°� �����ϱ�
}
