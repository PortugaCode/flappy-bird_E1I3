using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public static GroundSpawner instance; // �̱��� ���� ����

    [Header("Ground")]
    [SerializeField] private GameObject groundPrefab; // �׶��� ������Ʈ ������

    [Header("Object Pool")]
    [SerializeField] private GameObject objectPool; // ������Ʈ Ǯ
    public Queue<GameObject> grounds; // ������ �׶������ ���� ť
    private int groundCount = 4; // ������ �׶����� ����

    private Vector3 spawnPosition; // �׶��带 ������ ��ġ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnPosition = new Vector3(0, -7.25f, 135.0f); // �׶��� ���� ��ġ ����

        grounds = new Queue<GameObject>(); // ť �ν��Ͻ� ����

        CreateGround(); // �׶��� �̸� ����
        SetGround(); // �⺻ �׶��� ����
    }

    private void CreateGround()
    {
        for (int i = 0; i < groundCount; i++) // �׶��� �̸� �����ϰ� ��Ȱ��ȭ�Ͽ� ť�� ����
        {
            GameObject ground = Instantiate(groundPrefab);
            ground.transform.SetParent(objectPool.transform);
            ground.SetActive(false);
            grounds.Enqueue(ground);
        }
    }

    public void SpawnGround() // ť���� �׶��� ���� Ȱ��ȭ�ϰ� ��ġ ����
    {
        if (grounds.Count > 0)
        {
            GameObject ground = grounds.Dequeue();
            ground.SetActive(true);
            ground.transform.position = spawnPosition;
        }
    }

    private void SetGround() // ������ �� �⺻ Ground Ȱ��ȭ
    {
        int num = grounds.Count;

        for (int i = 0; i < num; i++)
        {
            GameObject ground = grounds.Dequeue();
            ground.SetActive(true);
            ground.transform.position = new Vector3(0, -7.25f, i * 45.0f);
        }
    }
}
