using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_oh : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float JumpForce;
    protected float horizontal;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }
}
