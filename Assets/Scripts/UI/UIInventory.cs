using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots; //아이템정보 
    public GameObject inventoryWindow; //인벤토리 창
    public Transform slotPanel;  //판넬
    public Transform dropPosition;

    [Header("Selected Item")]
    public ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    private PlayerController controller;  //정보를 주고받을 가지고오겠다
    private PlayerCondition condition;


    ItemData selecteditem;
    int selecteditemidx;

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount]; //slotPanel.childCount 안에 자식들이 얼마나 있는지 알수있다


        for (int i = 0; i < slots.Length; i++)  //for 문을 이용해 각각의 슬롯을 초기화 시켜준다
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            //slots[i].Clear();
        }

        ClearSelectedItemWindow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClearSelectedItemWindow()  //정보 초기화
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }
    public void Toggle()  //tab 키를 눌렀을때 나와줘야함 
    {
        if (IsOpen()) //켜져있음 
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()  //창이 열려있는지 아닌지 구분
    {
        return inventoryWindow.activeInHierarchy;
    }
    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;  //데이터 받아오기 

        //아이템 이 중복 가능한지  canStack 
        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)  // 비어있다면 // 비어있는 슬롯을 가져온다   
            {
                slot.quantity++;  //늘려주고
                UpdateUI();  //업데이트 해주고 
                CharacterManager.Instance.Player.itemData = null; // 초기화 해주고
                return; // 리턴해준다
            }
        }

        //비어있는 슬롯을 가져온다 
        ItemSlot emptySlot = GetEmptySlot();
        //있다면 
        if (emptySlot != null)
        {
            emptySlot.item = data;  //아이템에 데이터 넣어주고 
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }
        //없다면
        ThrowItem(data);  //아이템을 버려줘야한다 
        CharacterManager.Instance.Player.itemData = null; // 데이터 초기화
    }
    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * UnityEngine.Random.value * 360));
    }

    public void UpdateUI() //비어있을때 ui 세팅 해주는함수
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    private ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }


    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;  //인덱스 아이템이 널일때

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.item.consumables.Length; i++)
        {
            selectedItemStatName.text += selectedItem.item.consumables[i].type.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.item.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.item.type == ItemType.Equipable && slots[index].equipped);
        dropButton.SetActive(true);
    }
    public void OnUseButton()
    {
        if (selectedItem.item.type == ItemType.Consumable)
        {
            
                for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                switch (selectedItem.item.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.item.consumables[i].value); break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.item.consumables[i].value); break;
                }
            }
            RemoveSelctedItem();
        }
    }
    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelctedItem();
    }
    private void RemoveSelctedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            //if (slots[selectedItemIndex].equipped)
            //{
            //    UnEquip(selectedItemIndex);
            //}

            selectedItem.item = null;
            slots[selecteditemidx].item= null;
            selecteditemidx = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }
    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        CharacterManager.Instance.Player.equip.EquipNew(selectedItem.item);
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    public void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }
    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }
}
