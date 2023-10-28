using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public static GroundSpawner instance; // 싱글톤 변수 선언

    [Header("Ground")]
    [SerializeField] private GameObject groundPrefab; // 그라운드 오브젝트 프리팹

    [Header("Object Pool")]
    [SerializeField] private GameObject objectPool; // 오브젝트 풀
    public Queue<GameObject> grounds; // 생성한 그라운드들을 담을 큐
    private int groundCount = 4; // 생성할 그라운드의 개수

    private Vector3 spawnPosition; // 그라운드를 생성할 위치

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnPosition = new Vector3(0, -7.25f, 135.0f); // 그라운드 생성 위치 설정

        grounds = new Queue<GameObject>(); // 큐 인스턴스 생성

        CreateGround(); // 그라운드 미리 생성
        SetGround(); // 기본 그라운드 세팅
    }

    private void CreateGround()
    {
        for (int i = 0; i < groundCount; i++) // 그라운드 미리 생성하고 비활성화하여 큐에 저장
        {
            GameObject ground = Instantiate(groundPrefab);
            ground.transform.SetParent(objectPool.transform);
            ground.SetActive(false);
            grounds.Enqueue(ground);
        }
    }

    public void SpawnGround() // 큐에서 그라운드 꺼내 활성화하고 위치 설정
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
