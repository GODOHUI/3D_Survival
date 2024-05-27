using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour  //�������� �󸶳� �ٰ��� �󸶳� ���� �ٰ���

{

    public int damage;
    public float damageRate;

    private List<IDamagable> things = new List<IDamagable>(); //IDamagable�� �����س��� thigs���� &�ʱ�ȭ

    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamge(damage);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagalbe)) 
            //IDamagalbe�� ������������ �����ص״ٰ� DealDamage�� ����� �������� tackeDamge�Լ���ȣ���ϰ�
         {
           things.Add(damagalbe);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
        }
    }
}