using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : MonoBehaviour
{
    public float attackRate;  //공격주기 
    private bool attacking; //
    public float attackDistance; // 최대 공격 거리 

    [Header("Resource Gathering")]
    public bool doesGatherResources;  //리소스 체크 

    [Header("Combat")]
    public bool doesDealDamage;   // 공격이 가능한지 
    public int damage;   // 데미지  

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
