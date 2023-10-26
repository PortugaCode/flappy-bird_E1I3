using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject scoreTriggerPrefab;

    [Header("Object Pool")]
    [SerializeField] private GameObject objectPool;
    public Queue<GameObject> pipes;
    private int pipeCount = 21;

    public Queue<GameObject> scoreTriggers;
    private int scoreTriggerCount;

    [Header("Spawn")]
    public float spawnDistance = 5.0f;
    public float widthDistance = 3.5f;
    private Vector3[] spawnPositions = new Vector3[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnPositions[0] = new Vector3(transform.position.x - widthDistance, 0, pipeCount / 3 * spawnDistance - 2 * spawnDistance);
        spawnPositions[1] = new Vector3(transform.position.x, 0, pipeCount / 3 * spawnDistance - 2 * spawnDistance);
        spawnPositions[2] = new Vector3(transform.position.x + widthDistance, 0, pipeCount / 3 * spawnDistance - 2 * spawnDistance);

        pipes = new Queue<GameObject>();
        scoreTriggers = new Queue<GameObject>();

        scoreTriggerCount = pipeCount / 3;

        CreatePipe();
        CreateScoreTrigger();
        SetPipe();
    }

    private void CreatePipe()
    {
        for (int i = 0; i < pipeCount; i++)
        {
            GameObject pipe = Instantiate(pipePrefab);
            pipe.transform.SetParent(objectPool.transform);
            pipe.SetActive(false);
            pipes.Enqueue(pipe);
        }
    }

    private void CreateScoreTrigger()
    {
        for (int i = 0; i < scoreTriggerCount; i++)
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
            int open = Random.Range(0, 2);

            if (open == 0) // 0: open, 1: close
            {
                GameObject scoreTrigger = scoreTriggers.Dequeue();
                scoreTrigger.SetActive(true);
                scoreTrigger.transform.position = spawnPositions[1];

                for (int i = 0; i < 3; i++)
                {
                    GameObject pipe = pipes.Dequeue();
                    pipe.SetActive(true);
                    pipe.transform.position = spawnPositions[i];
                    pipe.GetComponent<Pipe>().SetRandomShape();
                }
            }
            else if (open == 1)
            {
                GameObject scoreTrigger = scoreTriggers.Dequeue();
                scoreTrigger.SetActive(true);
                scoreTrigger.transform.position = spawnPositions[1];

                int openNum = Random.Range(0, 3);

                for (int i = 0; i < 3; i++)
                {
                    GameObject pipe = pipes.Dequeue();
                    pipe.SetActive(true);
                    pipe.transform.position = spawnPositions[i];
                    pipe.GetComponent<Pipe>().SetRandomShape();

                    if (i != openNum)
                    {
                        pipe.GetComponent<Pipe>().ClosePipe();
                    }
                }
            }
        }
    }

    private void SetPipe() // 시작할 때 기본 Pipe 활성화
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
