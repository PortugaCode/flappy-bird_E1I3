using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Pipe")]
    [SerializeField] private GameObject upper; // 위 파이프
    [SerializeField] private GameObject lower; // 아래 파이프

    private float middlePosition; // 중간에 파이프 빈 곳의 위치

    private float upperScale; // 위 파이프의 스케일
    private float lowerScale; // 아래 파이프의 스케일

    [Header("Status")]
    public float moveSpeed = 5.0f; // 파이프 이동 속도

    private bool isEnd = false; // 끝까지 갔는지 확인 할 변수

    private void Update()
    {
        MoveForward(); // 파이프 이동
        CheckEnd(); // 끝까지 갔는지 확인 할 메서드
    }

    public void SetRandomShape() // 파이프 빈 곳의 위치 랜덤 설정
    {
        isEnd = false;

        middlePosition = Random.Range(-3.0f, 3.0f); // 빈 곳 -3.0 ~ 3.0 중 랜덤 설정

        upperScale = 6.0f - middlePosition * 1.2f + Mathf.Pow(middlePosition * 0.025f, 2); // 위 파이프 스케일 계산
        lowerScale = 6.0f + middlePosition * 1.2f - Mathf.Pow(middlePosition * 0.025f, 2); // 아래 파이프 스케일 계산

        upper.transform.localScale = new Vector3(upper.transform.localScale.x, upperScale, upper.transform.localScale.z); // 위 파이프 스케일 설정
        lower.transform.localScale = new Vector3(lower.transform.localScale.x, lowerScale, lower.transform.localScale.z); // 아래 파이프 스케일 설정
    }

    public void ClosePipe() // 해당 파이프에 빈 곳이 없게 스케일 설정
    {
        upper.transform.localScale = new Vector3(upper.transform.localScale.x, 7.5f, upper.transform.localScale.z);
        lower.transform.localScale = new Vector3(lower.transform.localScale.x, 7.5f, lower.transform.localScale.z);
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, -1.0f * moveSpeed * Time.deltaTime); // 파이프를 -z 축으로 이동
    }

    private void CheckEnd()
    {
        if (transform.position.z < -17.5f && !isEnd) // 끝까지 갔으면 비활성화 후 큐에 넣기
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
            if (collision.collider.GetComponent<PlayerController>().IsArmor) // 플레이어 컨트롤러 스크립트의 isArmor가 true 일때
            {
                isEnd = true;
                gameObject.SetActive(false);
                PipeSpawner.instance.pipes.Enqueue(gameObject);
                PipeSpawner.instance.SpawnPipe();
            }
        }
    }
}
