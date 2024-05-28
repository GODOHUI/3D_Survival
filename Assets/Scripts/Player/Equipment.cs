using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Equip
{
    public Equip curEquip;  //장착 정보 
    public Transform equipParent;  //장비를 달아줄 위치

    private PlayerController controller;
    private PlayerCondition condition;
    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EquipNew(ItemData data)  //장착 합수
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<Equip>();  //장착할 것을 넣어준다  
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
