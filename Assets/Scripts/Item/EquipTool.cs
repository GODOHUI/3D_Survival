using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;  //공격주기 
    private bool attacking; //
    public float attackDistance; // 최대 공격 거리 
    public float useStamina;

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
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }
    public override void OnAttackInput()  //애니매이션 동작시키는 함수
    {

        if (!attacking) //false일때만  attacking하고있는 와중에는 함수가 호출이 되더라도 안에 내부 호출 안되게 하는것 
        {
            if (CharacterManager.Instance.Player.condition.UseStamina(useStamina))
            {
                attacking = true;
                animator.SetTrigger("Attack");
                Invoke("OnCanAttack", attackRate);
            }
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }
    public void OnHit() //공격하는시점에 함수 호출

    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if (doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
            {
                resource.Gather(hit.point, hit.normal);
            }

            //if (doesDealDamage && hit.collider.TryGetComponent(out IDamagable damagable))
            //{
            //    damagable.TakePhysicalDamage(damage);
            //}
        }
    }
}
