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
        // ui 업데이트
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    { // 남은 체력 업데이트
        return curValue / maxValue;
        // 얘 이벤트로 만들 수 있을거같은데 잘 안되네
        // 필수 과제 끝내고 다시 해봐야겠다
    }

    public void Add(float inValue)
    { // 호출되면 해당 값 채우기
        curValue = Mathf.Min(curValue + inValue, maxValue);
    }

    public void Subtract(float inValue)
    { // 호출되면 해당 값 빼기
        curValue = Mathf.Max(curValue - inValue, 0.0f);
    }
    // 시간 나면 여기 싹 이벤트로 만드는거 도전하기
}
