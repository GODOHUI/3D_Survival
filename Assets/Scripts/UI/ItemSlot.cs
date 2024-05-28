using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item; // ����â�� ����  ������

    public UIInventory inventory;  //����
    public Button button; 
    public Image icon;
    public TextMeshProUGUI quatityText;
    private Outline outline;

    public int index;  // ������ ��ȣ
    public bool equipped;  //�����Ȱ��� �ƴ���
    public int quantity; //


    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = equipped; //���� �Ǿ�����
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

        if (outline != null)  //����ڵ�
        {
            outline.enabled = equipped;
        }
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
