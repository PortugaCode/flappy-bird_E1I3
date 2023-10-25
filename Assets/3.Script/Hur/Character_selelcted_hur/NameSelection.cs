using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameSelection : MonoBehaviour
{
    private GameObject[] nameList;
    private int index;
    public bool ready = false;

    private void Start()
    {
        nameList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            nameList[i] = transform.GetChild(i).gameObject;
        }

        //��������Ʈ ���� ����
        foreach (GameObject go in nameList)
        {
            go.SetActive(false);
        }

        //ù��° ĳ���� ��������Ʈ ���� �ѱ�
        if (nameList[0])
        {
            nameList[0].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        //Toggle off the current model
        nameList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = nameList.Length - 1;
        }

        //Toggle on the new model
        nameList[index].SetActive(true);
    }
    public void ToggleRight()
    {
        //Toggle off the current model
        nameList[index].SetActive(false);

        index++;
        if (index == nameList.Length)
        {
            index = 0;
        }

        //Toggle on the new model
        nameList[index].SetActive(true);
    }
    
}
