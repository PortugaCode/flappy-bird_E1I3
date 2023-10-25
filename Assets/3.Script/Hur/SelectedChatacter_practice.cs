using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SelectedChatacter_practice : MonoBehaviour
{
    private Animator anim;

    private GameObject[] characterList;
    private int index;
    public bool ready = false;

    
    private void Start()
    {
        characterList = new GameObject[transform.childCount];
        anim = GetComponent<Animator>(); 

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        //스프라이트 렌더 끄기
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        //첫번째 캐릭은 스프라이트 렌더 켜기
        if (characterList[0])
        {
            characterList[0].SetActive(true);
        }

    }

    public void ToggleLeft()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        //Toggle on the new model
        characterList[index].SetActive(true);
    }
    public void ToggleRight()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
        {
            index = 0;
        }

        //Toggle on the new model
        characterList[index].SetActive(true);
    }
    public void Selected_char()
    {
        Debug.Log("이 캐릭터가 선택되었습니다");
        ready = true;

        //practice
        anim.SetTrigger("Gogame");
    }

}
