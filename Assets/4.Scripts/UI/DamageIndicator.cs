using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    // ������ �̹���
    public float flashSpeed;
    // ȭ�� ������ �ð�

    private Coroutine coroutine;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage += Flash;
        // �÷��̾ ȭ���� �Ծ��� �� ����� �Լ��� �̺�Ʈ�� ����
    }

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
            // �ߺ� ���� ������ ���� �ʱ�ȭ
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
            // �� ������ �ڿ� ����
        }

        image.enabled = false;
        coroutine = null;
    }
}
