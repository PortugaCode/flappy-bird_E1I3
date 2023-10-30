using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] Player;
    public GameObject clone;
    


    // 캐릭터 선택 창 받아오는 역할
    private void Awake()
    {
        if(CharacterManager.instance.curr_Animal == Animal.bird)
        {
            clone = Instantiate(Player[0], transform.position, Quaternion.identity);
        }
        else if(CharacterManager.instance.curr_Animal == Animal.fish)
        {
            clone = Instantiate(Player[1], transform.position, Quaternion.identity);
        }
        else if (CharacterManager.instance.curr_Animal == Animal.monkey)
        {
            clone = Instantiate(Player[2], transform.position, Quaternion.identity);
        }

    }
}
