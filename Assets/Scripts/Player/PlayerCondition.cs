using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamagable
{
    void TakePhysicalDamge(int damage);

}

public class PlayerCondition : MonoBehaviour , IDamagable // 업데이트 반환
{
   
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }


    public float noHungerHealthDecay;
    public event Action onTakeDamage;  //데미지 받으면 이미지 변환
                                       //DamageIndicator  에서 PlayerCondition 에 접근해서 이벤트 등록

    void Update()  
    {
        //배고픔을 지속적으로 줄여보자  //Time.deltaTime 은 프레임을 일정하게 
        hunger.Subtract(hunger.passiveValue*Time.deltaTime);
        stamina.Add(stamina.passiveValue*Time.deltaTime);

        //만약에  hunger.curvalue가 0보다 작을때 haelth d에 있는 

        if (hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }
        else if(health.curValue == 0f)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    private void Die()
    {
        Debug.Log("죽었다");
    }

    public void TakePhysicalDamge(int damage)
    {

        health.Subtract(damage);
        onTakeDamage?.Invoke();

    }
    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
