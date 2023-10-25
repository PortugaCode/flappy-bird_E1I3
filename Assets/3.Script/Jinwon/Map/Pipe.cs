using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Pipe")]
    [SerializeField] private GameObject upper;
    [SerializeField] private GameObject lower;

    private float middlePosition;

    private float upperScale;
    private float lowerScale;

    [Header("Status")]
    public float moveSpeed = 5.0f;

    private bool isEnd = false;

    private void Update()
    {
        MoveForward();
        CheckEnd();
    }

    public void SetRandomShape()
    {
        isEnd = false;

        middlePosition = Random.Range(-3.0f, 3.0f);

        upperScale = 6.0f - middlePosition * 1.2f + Mathf.Pow(middlePosition * 0.025f, 2);
        lowerScale = 6.0f + middlePosition * 1.2f - Mathf.Pow(middlePosition * 0.025f, 2);

        upper.transform.localScale = new Vector3(upper.transform.localScale.x, upperScale, upper.transform.localScale.z);
        lower.transform.localScale = new Vector3(lower.transform.localScale.x, lowerScale, lower.transform.localScale.z);
    }

    public void ClosePipe()
    {
        upper.transform.localScale = new Vector3(upper.transform.localScale.x, 7.5f, upper.transform.localScale.z);
        lower.transform.localScale = new Vector3(lower.transform.localScale.x, 7.5f, lower.transform.localScale.z);
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, -1.0f * moveSpeed * Time.deltaTime);
    }

    private void CheckEnd()
    {
        if (transform.position.z < -15.0f && !isEnd)
        {
            isEnd = true;
            gameObject.SetActive(false);
            PipeSpawner.instance.pipes.Enqueue(gameObject);
            PipeSpawner.instance.SpawnPipe();
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.GetComponent<PlayerController>().isArmor) // 플레이어 컨트롤러 스크립트의 isArmor가 true 일때
            {
                isEnd = true;
                gameObject.SetActive(false);
                PipeSpawner.instance.pipes.Enqueue(gameObject);
                PipeSpawner.instance.SpawnPipe();
            }
        }
    }*/
}
