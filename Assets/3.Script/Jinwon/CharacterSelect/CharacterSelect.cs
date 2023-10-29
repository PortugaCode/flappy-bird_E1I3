using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animal
{
    bird, fish, monkey
}

public class CharacterSelect : MonoBehaviour
{
    public List<GameObject> animals;
    
    private int index = 0;

    private void Start()
    {
        foreach (GameObject go in animals)
        {
            go.SetActive(false);
        }

        animals[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        //Toggle off the current model
        animals[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = animals.Count - 1;
        }

        //Toggle on the new model
        animals[index].SetActive(true);
    }
    public void ToggleRight()
    {
        //Toggle off the current model
        animals[index].SetActive(false);

        index++;
        if (index == animals.Count)
        {
            index = 0;
        }

        //Toggle on the new model
        animals[index].SetActive(true);
    }

    public void Selected_char()
    {
        CharacterManager.instance.curr_Animal = (Animal)index;
        Debug.Log(CharacterManager.instance.curr_Animal);
    }
}
