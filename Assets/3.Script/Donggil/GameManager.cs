using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private TestUI_Donggil testui;
    private void Start()
    {
        testui = FindObjectOfType<TestUI_Donggil>();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveName();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadScore();
        }


    }

    public int current_score = 0;
    public string current_name = string.Empty;
    public GameObject inputField;

    public void Score(int s)
    {
        current_score += s;
        testui.UpdateScore();
    }


    public void SaveName()
    {
        inputField.SetActive(true);
        testui.InputName();
        
    }

    public void SaveScore()
    {
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;
        if (current_name != string.Empty)
        {
            string filename = "Score" + filecount + ".json";
            filename = Path.Combine("TestJson/", filename);
            string toJson = JsonConvert.SerializeObject(new JsonTest(current_score, current_name), Formatting.Indented);

            File.WriteAllText(filename, toJson);
            Debug.Log("Save " + filename);
            current_name = string.Empty;        //이름 초기화
        }
    }

    public List<JsonTest> test = new List<JsonTest>();
    public void LoadScore()
    {
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //파일 개수
        test.RemoveRange(0, test.Count);                //리스트 초기화(전부 삭제)

        for (int i = 0; i < filecount; i++)
        {
            string path = "TestJson/" + "Score" + i + ".json";
            string json = File.ReadAllText(path);
            JsonTest loadscore = JsonConvert.DeserializeObject<JsonTest>(json);
            test.Add(loadscore);
            Debug.Log($"test[{i}].Score = " + test[i].Score);

        }
        JsonTest temp = new JsonTest(0, "");        //임시값
        for (int i = 0; i < filecount - 1; i++)
        {
            for (int j = i + 1; j < filecount; j++)
            {
                if (test[i].Score < test[j].Score)
                {
                    temp = test[i];
                    test[i] = test[j];
                    test[j] = temp;
                }
            }
        }
        testui.UpdateRanking();
        Debug.Log("FileCount : " + filecount);
    }
}
[System.Serializable]
public class JsonTest
{
    public int Score;
    public string name;

    public JsonTest(int score, string name)
    {
        Score = score;
        this.name = name;
    }
}
