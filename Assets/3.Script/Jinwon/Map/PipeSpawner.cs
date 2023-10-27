using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner instance; // 싱글톤 인스턴스 선언

    [Header("Prefabs")]
    [SerializeField] private GameObject pipePrefab; // 파이프 오브젝트 프리팹
    [SerializeField] private GameObject scoreTriggerPrefab; // 점수 획득용 오브젝트 프리팹

    [Header("Object Pool")]
    [SerializeField] private GameObject objectPool; // 오브젝트 풀
    public Queue<GameObject> pipes; // 생성할 파이프들을 담을 큐
    private int pipeCount = 21; // 생성할 파이프 개수

    public Queue<GameObject> scoreTriggers; // 생성할 점수 획득용 오브젝트들을 담을 큐
    private int scoreTriggerCount; // 생성할 점수 획득용 오브젝트들의 개수

    [Header("Spawn")]
    public float spawnDistance = 5.0f; // 생성 위치까지의 거리
    public float widthDistance = 3.5f; // 파이프 사이의 간격
    private Vector3[] spawnPositions = new Vector3[3]; // 파이프를 생성할 위치 배열

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // 파이프 생성 위치 지정
        spawnPositions[0] = new Vector3(transform.position.x - widthDistance, 0, pipeCount / 3 * spawnDistance - 2 * spawnDistance);
        spawnPositions[1] = new Vector3(transform.position.x, 0, pipeCount / 3 * spawnDistance - 2 * spawnDistance);
        spawnPositions[2] = new Vector3(transform.position.x + widthDistance, 0, pipeCount / 3 * spawnDistance - 2 * spawnDistance);

        pipes = new Queue<GameObject>(); // 큐 인스턴스 생성
        scoreTriggers = new Queue<GameObject>(); // 큐 인스턴스 생성

        scoreTriggerCount = pipeCount / 3; // 점수 획득용 오브젝트 개수 = 파이프 개수 / 3

        CreatePipe(); // 파이프 생성
        CreateScoreTrigger(); // 점수 획득용 오브젝트 생성
        SetPipe(); // 기본 파이프 세팅
    }

    private void CreatePipe()
    {
        for (int i = 0; i < pipeCount; i++) // 파이프 미리 생성하고 비활성화하여 큐에 저장
        {
            GameObject pipe = Instantiate(pipePrefab);
            pipe.transform.SetParent(objectPool.transform);
            pipe.SetActive(false);
            pipes.Enqueue(pipe);
        }
    }

    private void CreateScoreTrigger()
    {
        for (int i = 0; i < scoreTriggerCount; i++) // 점수 획득용 오브젝트 미리 생성하고 비활성화하여 큐에 저장
        {
            GameObject scoreTrigger = Instantiate(scoreTriggerPrefab);
            scoreTrigger.transform.SetParent(objectPool.transform);
            scoreTrigger.SetActive(false);
            scoreTriggers.Enqueue(scoreTrigger);
        }
    }

    public void SpawnPipe()
    {
        if (pipes.Count > 3)
        {
            // 점수 획득용 오브젝트 큐에서 꺼내 활성화 후 위치 설정
            GameObject scoreTrigger = scoreTriggers.Dequeue();
            scoreTrigger.SetActive(true);
            scoreTrigger.transform.position = spawnPositions[1];

            int open = Random.Range(0, 2); // 세 파이프가 모두 비어있을지, 한 파이프만 비어있을지 랜덤 결정

            if (open == 0) // 세 파이프가 모두 빈 공간이 있는 경우
            {
                for (int i = 0; i < 3; i++) // 파이프 3개 큐에서 꺼내 활성화 후 위치 지정
                {
                    GameObject pipe = pipes.Dequeue();
                    pipe.SetActive(true);
                    pipe.transform.position = spawnPositions[i];
                    pipe.GetComponent<Pipe>().SetRandomShape();
                }
            }
            else if (open == 1) // 한 파이프만 빈 공간이 있는 경우
            {
                int openNum = Random.Range(0, 3); // 어떤 파이프에 빈 공간이 있을지 랜덤 결정

                for (int i = 0; i < 3; i++) // 파이프 3개 큐에서 꺼내 활성화 후 위치 지정
                {
                    GameObject pipe = pipes.Dequeue();
                    pipe.SetActive(true);
                    pipe.transform.position = spawnPositions[i];
                    pipe.GetComponent<Pipe>().SetRandomShape();

                    if (i != openNum)
                    {
                        pipe.GetComponent<Pipe>().ClosePipe(); // 두 파이프 빈 공간이 없게 닫기
                    }
                }
            }
        }
    }

    private void SetPipe() // 시작할 때 기본 Pipe 활성화, SpawnPipe와 로직 동일
    {
        int pipeNum = pipes.Count / 3;

        for (int i = 0; i < pipeNum; i++)
        {
            int open = Random.Range(0, 2);

            if (open == 0)
            {
                GameObject scoreTrigger = scoreTriggers.Dequeue();
                scoreTrigger.SetActive(true);
                scoreTrigger.transform.position = new Vector3(spawnPositions[1].x, 0, i * spawnDistance);

                for (int j = 0; j < 3; j++)
                {
                    GameObject pipe = pipes.Dequeue();
                    pipe.SetActive(true);
                    pipe.transform.position = new Vector3(spawnPositions[j].x, 0, i * spawnDistance);
                    pipe.GetComponent<Pipe>().SetRandomShape();
                }
            }
            else if (open == 1)
            {
                GameObject scoreTrigger = scoreTriggers.Dequeue();
                scoreTrigger.SetActive(true);
                scoreTrigger.transform.position = new Vector3(spawnPositions[1].x, 0, i * spawnDistance);

                int openNum = Random.Range(0, 3);

                for (int j = 0; j < 3; j++)
                {
                    GameObject pipe = pipes.Dequeue();
                    pipe.SetActive(true);
                    pipe.transform.position = new Vector3(spawnPositions[j].x, 0, i * spawnDistance);
                    pipe.GetComponent<Pipe>().SetRandomShape();

                    if (j != openNum)
                    {
                        pipe.GetComponent<Pipe>().ClosePipe();
                    }
                }
            }
        }
    }
}
