using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;  //현재값
    public float startValue; // 시작값
    public float maxValue; //최대값 
    public float passiveValue; //변화값
    public Image uiBar;



    private void Start()
    {
        curValue =startValue;
    }
    private void Update()
    {
        //ui 업데이트 
        uiBar.fillAmount = GetPercentage();

    }
     float GetPercentage() { return curValue / maxValue; }

    public void Add(float value)  //함수가 호출되면 curValue에 더해지게된다
    {
        curValue = Mathf.Min(curValue+ value,maxValue);
    }

    public void Subtract (float value) // 데미지 를받았을때 함수 호출
    { curValue = Mathf.Max(curValue- value, 0f); }


}
