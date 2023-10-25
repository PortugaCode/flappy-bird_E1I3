using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Status")]
    public float moveSpeed = 5.0f;

    private bool isEnd = false;

    private void OnEnable()
    {
        isEnd = false;
    }

    private void Update()
    {
        MoveForward();
        CheckEnd();
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, -1.0f * moveSpeed * Time.deltaTime);
    }

    private void CheckEnd()
    {
        if (transform.position.z < -45.0f && !isEnd)
        {
            isEnd = true;
            gameObject.SetActive(false);
            GroundSpawner.instance.grounds.Enqueue(gameObject);
            GroundSpawner.instance.SpawnGround();
        }
    }
}
