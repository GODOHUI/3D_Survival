using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour  //데미지를 얼마나 줄건지 얼마나 자주 줄건지

{

    public int damage;
    public float damageRate;

    private List<IDamagable> things = new List<IDamagable>(); //IDamagable을 저장해놓은 thigs선언 &초기화

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
            //IDamagalbe을 가지고있으면 보관해뒀다가 DealDamage에 저장된 만들어놨던 tackeDamge함수를호출하게
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
