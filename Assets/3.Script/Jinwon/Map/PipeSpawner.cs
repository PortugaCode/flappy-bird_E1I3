using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner instance;

    [Header("Pipe")]
    [SerializeField] private GameObject pipePrefab;

    [Header("Object Pool")]
    public Queue<GameObject> pipes;
    private int pipeCount = 15;

    [Header("Spawn")]
    public float spawnCooldown = 1.5f;
    public float spawnDistance = 5.0f;
    private Vector3[] spawnPositions = new Vector3[3];
    private WaitForSeconds wfs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnPositions[0] = new Vector3(transform.position.x - 3.0f, 0, spawnDistance);
        spawnPositions[1] = new Vector3(transform.position.x, 0, spawnDistance);
        spawnPositions[2] = new Vector3(transform.position.x + 3.0f, 0, spawnDistance);

        wfs = new WaitForSeconds(spawnCooldown);

        pipes = new Queue<GameObject>();

        CreatePipe();
        StartCoroutine(StartSpawn_co());
    }

    private IEnumerator StartSpawn_co()
    {
        while (true)
        {
            SpawnPipe();
            yield return wfs;
        }
    }

    private void CreatePipe()
    {
        for (int i = 0; i < pipeCount; i++)
        {
            GameObject pipe = Instantiate(pipePrefab);
            pipe.SetActive(false);
            pipes.Enqueue(pipe);
        }
    }

    private void SpawnPipe()
    {
        if (pipes.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject pipe = pipes.Dequeue();
                pipe.SetActive(true);
                pipe.transform.position = spawnPositions[i];
                pipe.GetComponent<Pipe>().SetRandomShape();
            }
        }
    }
}
