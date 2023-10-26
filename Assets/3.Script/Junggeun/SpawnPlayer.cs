using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] Player;

    private void Start()
    {
        GameObject clone = Instantiate(Player[0], transform.position, Quaternion.identity);
    }
}
