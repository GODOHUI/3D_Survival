using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public void OnAttackInput(InputAction.CallbackContext context)  //마우스 클릭했을때 움직이게하는것
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && controller.canLook) //controller.canLook 인벤토리가 꺼져있을때 동작해야한다
        {
            curEquip.OnAttackInput();
        }
    }
}
