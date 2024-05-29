using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : Equip
{
    public Equip curEquip;  //���� ���� 
    public Transform equipParent;  //��� �޾��� ��ġ

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
    public void EquipNew(ItemData data)  //���� �ռ�
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<Equip>();  //������ ���� �־��ش�  
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
    public void OnAttackInput(InputAction.CallbackContext context)  //���콺 Ŭ�������� �����̰��ϴ°�
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && controller.canLook) //controller.canLook �κ��丮�� ���������� �����ؾ��Ѵ�
        {
            curEquip.OnAttackInput();
        }
    }
}
