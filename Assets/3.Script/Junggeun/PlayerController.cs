using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerMovement_oh
{
    private Rigidbody rig;
    private Animator animator;
    private bool isDie;

    private int Maxhp = 1;
    private int Curhp;

    //Skill ����
    private bool isArmor = false;
    private bool isArmorCoolDown = false;
    public bool IsArmor => isArmor;

    //�� �� �¾ƾ� �״°� => TakeDamege �޼ҵ�� �ذ�

    //������ �뽬

    private void Awake()
    {
        Curhp = Maxhp;
        isDie = false;
        TryGetComponent(out rig);
        TryGetComponent(out animator);
    }

    private void FixedUpdate()
    {
        if (isDie) return;
        rig.velocity = new Vector3(horizontal * MoveSpeed * Time.deltaTime, rig.velocity.y, rig.velocity.z);
    }

    private void Update()
    {
        animator.SetBool("Die", isDie);
        animator.SetBool("Armor", isArmor);
        if (isDie) return;


        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") && !isArmor)
        {
            rig.velocity = Vector3.zero;
            rig.AddForce(new Vector3(rig.velocity.x, JumpForce, rig.velocity.z));
            animator.SetTrigger("Jump");
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isArmorCoolDown)
        {
            StartCoroutine(ArmorSkill());
        }
    }
    
    private IEnumerator ArmorSkill()
    {
        isArmor = true;
        isArmorCoolDown = true;
        rig.velocity = Vector3.zero;
        rig.useGravity = false;
        //ignoreLayer ���� �ֱ�
        yield return new WaitForSeconds(5f);
        //ignoreLayer �� �ֱ�
        isArmor = false;
        rig.useGravity = true;
        yield return new WaitForSeconds(7f);
        isArmorCoolDown = false;
    }




    //������ �޼ҵ�
    public void TakeDamege(int damege = 1)
    {
        Curhp -= damege;
        if(Curhp <= 0)
        {
            isDie = true;
        }
        animator.SetTrigger("Hit");
    }
}
