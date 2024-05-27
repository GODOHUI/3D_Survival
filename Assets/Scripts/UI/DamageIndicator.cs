using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;  //�̹��� ����
    public float flashSpeed; //�̹��� �ӵ�

    private Coroutine coroutine;

   
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }
    public void Flash()
    {
        if (coroutine != null) //�ڷ�ƾ�� ���� �Ǿ��������� �ִ�
        {
            StopCoroutine(coroutine);
            Debug.Log("??? ");
        }
        Debug.Log("������ ");
        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f);
        coroutine = StartCoroutine(FadeAway());  //StartCoroutine �� ���� �ڷ�ƾ ��ȯ

    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0.0f)
        {
            Debug.Log("�����̵Ǿ���ؿ�... ");
            a -= (startAlpha/flashSpeed)*Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 105f / 255f, a);
            yield return null;
        }
        image.enabled = false; //������ ������ ���ֱ�
    }
    }
