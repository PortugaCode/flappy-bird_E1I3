using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Status")]
    public float moveSpeed = 5.0f; // ������Ʈ �̵� �ӵ�

    private bool isEnd = false; // ������ ������ Ȯ���� ����

    private void OnEnable()
    {
        isEnd = false; // ������Ʈ Ȱ��ȭ �� ���� �ʱ�ȭ
    }

    private void Update()
    {
        MoveForward(); // ������Ʈ �̵�
        CheckEnd(); // ������ ������ Ȯ���� �޼���
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, -1.0f * moveSpeed * Time.deltaTime); // -z �������� �̵�
    }

    private void CheckEnd()
    {
        if (transform.position.z < -45.0f && !isEnd) // ������ �� ��� ������Ʈ Ǯ�� �ֱ�
        {
            isEnd = true;
            gameObject.SetActive(false);
            GroundSpawner.instance.grounds.Enqueue(gameObject);
            GroundSpawner.instance.SpawnGround();
        }
    }
}
