using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : MonoBehaviour
{
    public float attackRate;  //�����ֱ� 
    private bool attacking; //
    public float attackDistance; // �ִ� ���� �Ÿ� 

    [Header("Resource Gathering")]
    public bool doesGatherResources;  //���ҽ� üũ 

    [Header("Combat")]
    public bool doesDealDamage;   // ������ �������� 
    public int damage;   // ������  

    private Animator animator;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
