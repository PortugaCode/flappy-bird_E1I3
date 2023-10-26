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

    //Skill 무적
    private bool isArmor = false;
    private bool isArmorCoolDown = false;
    public bool IsArmor => isArmor;

    //두 번 맞아야 죽는거 => TakeDamege 메소드로 해결

    //옆으로 대쉬
    private bool isRun = false;
    private bool isRunCoolDown = false;
    public bool IsRun => isRun;

    private CapsuleCollider coll;

    private void Awake()
    {
        Curhp = Maxhp;
        isDie = false;
        TryGetComponent(out rig);
        TryGetComponent(out animator);
        TryGetComponent(out coll);
    }

    private void FixedUpdate()
    {
        if (isDie || isRun) return;
        rig.velocity = new Vector3(horizontal * MoveSpeed * Time.deltaTime, rig.velocity.y, rig.velocity.z);
    }

    private void Update()
    {
        animator.SetBool("Die", isDie);
        animator.SetBool("Armor", isArmor);
        animator.SetBool("Run", isRun);
        if (isDie) return;


        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") && !isArmor &&!isRun)
        {
            rig.velocity = Vector3.zero;
            rig.AddForce(new Vector3(rig.velocity.x, JumpForce, rig.velocity.z));
            animator.SetTrigger("Jump");
            AudioManager.Instance.PlaySFX("Jump");
        }

        //무적 스킬 ====================================================
        //if(Input.GetKeyDown(KeyCode.Space) && !isArmorCoolDown)
        //{
        //    StartCoroutine(ArmorSkill());
        //}
        //-==============================================================


        //대쉬 스킬======================================================
        if(Input.GetKeyDown(KeyCode.Space) && !isRunCoolDown)
        {
            StartCoroutine(RunSkill());
        }
        //-==============================================================
    }

    private IEnumerator RunSkill()
    {
        isRun = true;
        isRunCoolDown = true;
        rig.velocity = Vector3.zero;
        rig.useGravity = false;
        rig.AddForce(new Vector3(horizontal * MoveSpeed+100f * Time.deltaTime, rig.velocity.y, rig.velocity.z));
        AudioManager.Instance.PlaySFX("Dash");
        yield return new WaitForSeconds(0.4f);
        rig.useGravity = true;
        isRun = false;
    
        yield return new WaitForSeconds(5f);
        isRunCoolDown = false;
    }



    private IEnumerator ArmorSkill()
    {
        isArmor = true;
        isArmorCoolDown = true;
        rig.velocity = Vector3.zero;
        rig.useGravity = false;
        //ignoreLayer 시작 넣기
        AudioManager.Instance.PlaySFX("Dash");
        yield return new WaitForSeconds(5f);
        //ignoreLayer 끝 넣기
        isArmor = false;
        rig.useGravity = true;
        yield return new WaitForSeconds(7f);
        isArmorCoolDown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pipe"))
        {
            TakeDamege();
            Debug.Log("닿았다");
        }
    }


    //데미지 메소드
    public void TakeDamege(int damege = 1)
    {
        Debug.Log("데미지 받았다.");

        Curhp -= damege;
        if(Curhp <= 0)
        {
            isDie = true;
            coll.isTrigger = true;
        }
        animator.SetTrigger("Hit");
        AudioManager.Instance.PlaySFX("Hit");
    }


}
