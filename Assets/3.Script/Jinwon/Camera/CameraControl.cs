using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - target.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, 5.0f * Time.deltaTime);
    }
}
