using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Pipe")]
    [SerializeField] private GameObject upper; // �� ������
    [SerializeField] private GameObject lower; // �Ʒ� ������

    private float middlePosition; // �߰��� ������ �� ���� ��ġ

    private float upperScale; // �� �������� ������
    private float lowerScale; // �Ʒ� �������� ������

    [Header("Status")]
    public float moveSpeed = 5.0f; // ������ �̵� �ӵ�

    private bool isEnd = false; // ������ ������ Ȯ�� �� ����

    private void Update()
    {
        MoveForward(); // ������ �̵�
        CheckEnd(); // ������ ������ Ȯ�� �� �޼���
    }

    public void SetRandomShape() // ������ �� ���� ��ġ ���� ����
    {
        isEnd = false;

        middlePosition = Random.Range(-3.0f, 3.0f); // �� �� -3.0 ~ 3.0 �� ���� ����

        upperScale = 6.0f - middlePosition * 1.2f + Mathf.Pow(middlePosition * 0.025f, 2); // �� ������ ������ ���
        lowerScale = 6.0f + middlePosition * 1.2f - Mathf.Pow(middlePosition * 0.025f, 2); // �Ʒ� ������ ������ ���

        upper.transform.localScale = new Vector3(upper.transform.localScale.x, upperScale, upper.transform.localScale.z); // �� ������ ������ ����
        lower.transform.localScale = new Vector3(lower.transform.localScale.x, lowerScale, lower.transform.localScale.z); // �Ʒ� ������ ������ ����
    }

    public void ClosePipe() // �ش� �������� �� ���� ���� ������ ����
    {
        upper.transform.localScale = new Vector3(upper.transform.localScale.x, 7.5f, upper.transform.localScale.z);
        lower.transform.localScale = new Vector3(lower.transform.localScale.x, 7.5f, lower.transform.localScale.z);
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, -1.0f * moveSpeed * Time.deltaTime); // �������� -z ������ �̵�
    }

    private void CheckEnd()
    {
        if (transform.position.z < -17.5f && !isEnd) // ������ ������ ��Ȱ��ȭ �� ť�� �ֱ�
        {
            isEnd = true;
            gameObject.SetActive(false);
            PipeSpawner.instance.pipes.Enqueue(gameObject);
            PipeSpawner.instance.SpawnPipe();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.GetComponent<PlayerController>().IsArmor) // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� isArmor�� true �϶�
            {
                isEnd = true;
                gameObject.SetActive(false);
                PipeSpawner.instance.pipes.Enqueue(gameObject);
                PipeSpawner.instance.SpawnPipe();
            }
        }
    }
}
