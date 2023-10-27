using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool isScored = false;
    private bool isEnd = false;

    [Header("Status")]
    public float moveSpeed = 5.0f;

    private BoxCollider boxCollider;

    private void Awake()
    {
        TryGetComponent(out boxCollider);
    }

    private void OnEnable()
    {
        isScored = false;
        isEnd = false;
        boxCollider.enabled = true;
    }

    private void Update()
    {
        MoveForward();
        CheckEnd();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isScored)
        {
            Debug.Log("Score");
            isScored = true;
            GameManager.instance.Score(100);
            boxCollider.enabled = false;
        }
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, -1.0f * moveSpeed * Time.deltaTime);
    }

    private void CheckEnd()
    {
        if (transform.position.z < -17.5f && !isEnd)
        {
            isEnd = true;
            gameObject.SetActive(false);
            PipeSpawner.instance.scoreTriggers.Enqueue(gameObject);
        }
    }
}
