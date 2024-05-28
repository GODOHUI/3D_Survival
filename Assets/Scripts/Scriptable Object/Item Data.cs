using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource,   //����
    Equipable,  //����
    Consumable  //�ڿ�
}

public enum ConsumableType  
{
    Hunger,   
    Health    
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;  //Ÿ��
    public float value;    // �Ծ������� ��
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;  //������ �̸�
    public string description; //������ ����
    public ItemType type;  // Ÿ��
    public Sprite icon;  //�̹���
    public GameObject dropPrefab;  //������

    [Header("Stacking")]
    public bool canStack;  //������ �������ִ� 
    public int maxStackAmount; //�󸶳� ������ �������ִ°�

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;   // �Һ� 

    [Header("Equip")]
    public GameObject equipPrefab;
}
