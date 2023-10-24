using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Lee : MonoBehaviour
{
    public float moveSpeed = 20f;
    private Rigidbody rigid;

    private void Awake()
    {
        TryGetComponent(out rigid);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveZ = Input.GetAxisRaw("Vertical");

        Vector3 X = Vector3.right * MoveX;
        Vector3 Z = Vector3.forward * MoveZ;
        rigid.MovePosition(transform.position + (X + Z) * moveSpeed * Time.deltaTime);
    }
}
