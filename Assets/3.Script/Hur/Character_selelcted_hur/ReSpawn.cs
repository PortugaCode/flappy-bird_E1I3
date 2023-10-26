using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;
    private void Start()
    {
        //player = Instantiate(charPrefabs[(int)CharacterManager.instance.curr_Animal]);
        //    player.transform.position = transform.position;
    }
}
