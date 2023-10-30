using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerMovement_oh
{
    private Rigidbody rig;
    private Animator animator;
    private bool isDie;
    public bool IsDie => isDie;

    private int Maxhp = 1;
    private int Curhp;

    //Skill ����
    private bool isArmor = false;
    private bool isArmorCoolDown = false;
    public bool IsArmor => isArmor;

    //�� �� �¾ƾ� �״°� => TakeDamege �޼ҵ�� �ذ�

    //������ �뽬
    private bool isRun = false;
    private bool isRunCoolDown = false;
    public bool IsRun => isRun;

    private CapsuleCollider coll;

    private void Awake()
    {
        // ������ ĳ���͸� �� 2 ������ִ� ����
        if(CharacterManager.instance.curr_Animal == Animal.monkey)
        {
            Maxhp = 2;
        }

        Curhp = Maxhp;
        isDie = false;
        TryGetComponent(out rig);
        TryGetComponent(out animator);
        TryGetComponent(out coll);
    }

    private void FixedUpdate()
    {
        if (isDie || isRun) return;
        rig.velocity = new Vector3(horizontal * MoveSpeed * Time.deltaTime, rig.velocity.y, rig.velocity.z); //ĳ���� ������
    }

    private void Update()
    {
        animator.SetBool("Die", isDie);
        animator.SetBool("Armor", isArmor);
        animator.SetBool("Run", isRun);
        if (isDie)
        {
            coll.isTrigger = true;
            return;
        }

        //���� ��ų && �뽬 ��ų ====================================================
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isArmorCoolDown && CharacterManager.instance.curr_Animal == Animal.bird)
            {
                StartCoroutine(ArmorSkill()); // ����
            }
            else if (!isRunCoolDown && CharacterManager.instance.curr_Animal == Animal.fish)
            {
                StartCoroutine(RunSkill()); // �뽬
            }
        }
        //-==============================================================

        //ĳ���� �¿� �� �Ʒ� �ִ� �̵� �Ÿ� ����========================
        float x = Mathf.Clamp(transform.position.x, -4.5f, 4.5f);
        float y = Mathf.Clamp(transform.position.y, -5.5f, 5.5f);
        transform.position = new Vector3(x, y, transform.position.z);
        //===============================================================


        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") && !isArmor &&!isRun)
        {
            rig.velocity = Vector3.zero;
            rig.AddForce(new Vector3(rig.velocity.x, JumpForce, rig.velocity.z));
            animator.SetTrigger("Jump");
            AudioManager.Instance.PlaySFX("Jump");
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && rig.velocity.y > 0)
        {
            rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y * 0.55f, rig.velocity.z);
        }



    }

    //Run Skill �ڷ�ƾ
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


    // ���� ��ų �ڷ�ƾ
    private IEnumerator ArmorSkill()
    {
        isArmor = true; // isArmor == true�� ��� ������ ����
        isArmorCoolDown = true;
        rig.velocity = Vector3.zero;
        rig.useGravity = false;
        //ignoreLayer ���� �ֱ�
        AudioManager.Instance.PlaySFX("Dash");
        yield return new WaitForSeconds(5f);
        //ignoreLayer �� �ֱ�
        isArmor = false;
        rig.useGravity = true;
        yield return new WaitForSeconds(7f);
        isArmorCoolDown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pipe") && gameObject.CompareTag("Player") && !isArmor)
        {
            TakeDamege();
            Debug.Log("��Ҵ�");
        }
    }


    //������ �޼ҵ�
    public void TakeDamege(int damege = 1)
    {
        Debug.Log("������ �޾Ҵ�.");

        Curhp -= damege;
        StartCoroutine(hitarmor());
        if(Curhp <= 0 && !isDie)
        {
            isDie = true;
            GameManager.instance.GameOver();
        }
        animator.SetTrigger("Hit");
        AudioManager.Instance.PlaySFX("Hit");
    }

    //Hit �� ��� ����
    private IEnumerator hitarmor()
    {
        coll.isTrigger = true;
        gameObject.tag = "ArmorPlayer"; // tag �ٲ㼭 ���������� ������ �Դ� �� ����
        yield return new WaitForSeconds(1f);
        coll.isTrigger = false;
        gameObject.tag = "Player";
    }
}
