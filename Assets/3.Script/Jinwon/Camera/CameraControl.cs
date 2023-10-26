using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject target;

    private Vector3 offset;

    private void Start()
    {
        target = FindObjectOfType<SpawnPlayer>().clone;
        offset = transform.position - target.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, 5.0f * Time.deltaTime);
    }
}
