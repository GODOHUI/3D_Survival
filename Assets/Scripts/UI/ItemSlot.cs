using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item; // 슬롯창에 넣을  아이템

    public UIInventory inventory;  //정보
    public Button button; 
    public Image icon;
    public TextMeshProUGUI quatityText;
    private Outline outline;

    public int index;  // 아이템 번호
    public bool equipped;  //장착된것지 아닌지
    public int quantity; //


    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = equipped; //장착 되었을때
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }


    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)  //방어코드
        {
            outline.enabled = equipped;
        }
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
