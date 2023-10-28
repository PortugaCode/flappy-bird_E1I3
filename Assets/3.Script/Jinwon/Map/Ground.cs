using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Status")]
    public float moveSpeed = 5.0f; // 오브젝트 이동 속도

    private bool isEnd = false; // 끝까지 갔는지 확인할 변수

    private void OnEnable()
    {
        isEnd = false; // 오브젝트 활성화 시 변수 초기화
    }

    private void Update()
    {
        MoveForward(); // 오브젝트 이동
        CheckEnd(); // 끝까지 갔는지 확인할 메서드
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, -1.0f * moveSpeed * Time.deltaTime); // -z 방향으로 이동
    }

    private void CheckEnd()
    {
        if (transform.position.z < -45.0f && !isEnd) // 끝까지 간 경우 오브젝트 풀에 넣기
        {
            isEnd = true;
            gameObject.SetActive(false);
            GroundSpawner.instance.grounds.Enqueue(gameObject);
            GroundSpawner.instance.SpawnGround();
        }
    }
}
