using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamagable
{
    void TakePhysicalDamge(int damage);

}

public class PlayerCondition : MonoBehaviour , IDamagable // ������Ʈ ��ȯ
{
   
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }


    public float noHungerHealthDecay;
    public event Action onTakeDamage;  //������ ������ �̹��� ��ȯ
                                       //DamageIndicator  ���� PlayerCondition �� �����ؼ� �̺�Ʈ ���

    void Update()  
    {
        //������� ���������� �ٿ�����  //Time.deltaTime �� �������� �����ϰ� 
        hunger.Subtract(hunger.passiveValue*Time.deltaTime);
        stamina.Add(stamina.passiveValue*Time.deltaTime);

        //���࿡  hunger.curvalue�� 0���� ������ haelth d�� �ִ� 

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
        Debug.Log("�׾���");
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
