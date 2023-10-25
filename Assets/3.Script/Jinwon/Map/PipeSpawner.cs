using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner instance;

    [Header("Pipe")]
    [SerializeField] private GameObject pipePrefab;

    [Header("Object Pool")]
    [SerializeField] private GameObject objectPool;
    public Queue<GameObject> pipes;
    private int pipeCount = 21;

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

        spawnPositions[0] = new Vector3(transform.position.x - widthDistance, 0, pipeCount / 3 * spawnDistance - spawnDistance);
        spawnPositions[1] = new Vector3(transform.position.x, 0, pipeCount / 3 * spawnDistance - spawnDistance);
        spawnPositions[2] = new Vector3(transform.position.x + widthDistance, 0, pipeCount / 3 * spawnDistance - spawnDistance);

        pipes = new Queue<GameObject>();

        CreatePipe();
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

    public void SpawnPipe()
    {
        if (pipes.Count > 3)
        {
            int open = Random.Range(0, 2);

            if (open == 0) // 0: open, 1: close
            {
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
