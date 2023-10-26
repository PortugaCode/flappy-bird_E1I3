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

    public Animal curr_Animal; //다음 플레이어 지정할 때 쓰는 변수

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
        #region 싱글톤
        //싱글톤
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
        //Enum값과 GameObj 매핑
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

        //방향키로 선택
        foreach (GameObject go in animals)
        {
            go.SetActive(false);
        }

        if (animals[0])//첫번째 캐릭은 스프라이트 렌더 켜기
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
        Debug.Log("이 캐릭터가 선택되었습니다");
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


