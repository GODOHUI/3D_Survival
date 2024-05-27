using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;  //���簪
    public float startValue; // ���۰�
    public float maxValue; //�ִ밪 
    public float passiveValue; //��ȭ��
    public Image uiBar;



    private void Start()
    {
        curValue =startValue;
    }
    private void Update()
    {
        //ui ������Ʈ 
        uiBar.fillAmount = GetPercentage();

    }
     float GetPercentage() { return curValue / maxValue; }

    public void Add(float value)  //�Լ��� ȣ��Ǹ� curValue�� �������Եȴ�
    {
        curValue = Mathf.Min(curValue+ value,maxValue);
    }

    public void Subtract (float value) // ������ ���޾����� �Լ� ȣ��
    { curValue = Mathf.Max(curValue- value, 0f); }


}
