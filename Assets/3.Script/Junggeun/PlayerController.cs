using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerMovement_oh
{
    private Rigidbody rig;
    private Animator animator;
    private bool isDie;

    private void Awake()
    {
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
        if (isDie) return;
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            rig.velocity = Vector3.zero;
            rig.AddForce(new Vector3(rig.velocity.x, JumpForce, rig.velocity.z));
            animator.SetTrigger("Jump");
        }
        /*        else if (Input.GetKeyUp(KeyCode.Space) && rig.velocity.y > 0f)
                {
                    rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y * 0.45f, rig.velocity.z);
                }*/
    }


    public void Die()
    {
        animator.SetTrigger("Die");
        isDie = true;
    }
}
