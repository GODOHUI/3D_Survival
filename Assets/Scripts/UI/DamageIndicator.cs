using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;  //이미지 접근
    public float flashSpeed; //이미지 속도

    private Coroutine coroutine;

   
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }
    public void Flash()
    {
        if (coroutine != null) //코루틴이 실행 되어있을수도 있다
        {
            StopCoroutine(coroutine);
            Debug.Log("??? ");
        }
        Debug.Log("아프다 ");
        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f);
        coroutine = StartCoroutine(FadeAway());  //StartCoroutine 을 통해 코루틴 반환

    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0.0f)
        {
            Debug.Log("실행이되어야해요... ");
            a -= (startAlpha/flashSpeed)*Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 105f / 255f, a);
            yield return null;
        }
        image.enabled = false; //문장이 끝나면 꺼주기
    }
    }
