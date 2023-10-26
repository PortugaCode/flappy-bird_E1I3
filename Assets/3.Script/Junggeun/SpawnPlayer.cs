using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] Player;
    public GameObject clone;

    private void Awake()
    {
        clone = Instantiate(Player[0], transform.position, Quaternion.identity);
    }
}
