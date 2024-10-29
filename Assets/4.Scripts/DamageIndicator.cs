using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    // 데미지 이미지
    public float flashSpeed;
    // 화상 데미지 시간

    private Coroutine coroutine;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage += Flash;
        // 플레이어가 화상을 입었을 때 사용할 함수를 이벤트에 저장
    }

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
            // 중복 실행 방지를 위한 초기화
        }

        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 105f / 255, a);

            yield return null;
            // 한 프레임 뒤에 실행
        }

        image.enabled = false;
        coroutine = null;
    }
}
