using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public static GroundSpawner instance;

    [Header("Ground")]
    [SerializeField] private GameObject groundPrefab;

    [Header("Object Pool")]
    [SerializeField] private GameObject objectPool;
    public Queue<GameObject> grounds;
    private int groundCount = 4;

    private Vector3 spawnPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnPosition = new Vector3(0, -7.25f, 135.0f);

        grounds = new Queue<GameObject>();

        CreateGround();
        SetGround();
    }

    private void CreateGround()
    {
        for (int i = 0; i < groundCount; i++)
        {
            GameObject ground = Instantiate(groundPrefab);
            ground.transform.SetParent(objectPool.transform);
            ground.SetActive(false);
            grounds.Enqueue(ground);
        }
    }

    public void SpawnGround()
    {
        if (grounds.Count > 0)
        {
            GameObject ground = grounds.Dequeue();
            ground.SetActive(true);
            ground.transform.position = spawnPosition;
        }
    }

    private void SetGround() // 시작할 때 기본 Ground 활성화
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
