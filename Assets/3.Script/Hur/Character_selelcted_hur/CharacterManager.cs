using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum Animal
{
    bird, fish, monkey 
}

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance = null;

    public List<GameObject> animals;
    private Dictionary<Animal, GameObject> enumToGameObjectMap = new Dictionary<Animal, GameObject>();

    public Animal curr_Animal; //���� �÷��̾� ������ �� ���� ����

    private Animator anim;
    private int index;
    public bool ready;

    public Ani_BirdControll bird;
    public Ani_FishControll fish;
    public Ani_MonkeyControll monkey;

    private GameObject selectedObject;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        #region �̱���
        //�̱���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        ready = false;
    }
    private void Start()
    {
        //Enum���� GameObj ����
        for(int i = 0; i < animals.Count; i++)
        {
            Animal enumValue = (Animal)i;
            enumToGameObjectMap[enumValue] = animals[i];
        }

        Animal selectedType = Animal.bird;
        selectedObject = enumToGameObjectMap[selectedType];

        if (selectedType != null)
        {
            selectedObject.SetActive(true);
        }

        //����Ű�� ����
        foreach (GameObject go in animals)
        {
            go.SetActive(false);
        }

        if (animals[0])//ù��° ĳ���� ��������Ʈ ���� �ѱ�
        {
            animals[0].SetActive(true);

        }

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
        ready = true;
        Debug.Log("�� ĳ���Ͱ� ���õǾ����ϴ�");
        curr_Animal = (Animal)index;
        switch ((Animal)index)
        {
            case Animal.bird:
                bird.Ready();
                break;
            case Animal.fish:
                fish.Ready();
                break;
            case Animal.monkey:
                monkey.Ready();
                break;

        }
    }
}


