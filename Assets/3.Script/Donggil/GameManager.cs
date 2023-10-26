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
    private bool isGameOver = false;






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

            DeleteFile();
        }


    }

    public int current_score = 0;
    public string current_name = string.Empty;
    

    public void Score(int s)
    {
        current_score += s;
        testui.UpdateScore();
    }


    public void SaveName()
    {
        
        testui.input.gameObject.SetActive(true);        //InputField 켜기
        testui.InputName();                             //이름 입력
    }

    public void SaveScore()
    {
        
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //파일개수 불러오기
        if (current_name != string.Empty)
        {
            string filename = "Score" + filecount + ".json";                    //파일 이름은 Score번호.json
            filename = Path.Combine("TestJson/", filename);
            string toJson = JsonConvert.SerializeObject(new JsonTest(current_score, current_name), Formatting.Indented);        //json 직렬화

            File.WriteAllText(filename, toJson);            //파일 쓰기
            Debug.Log("Save " + filename);
            current_name = string.Empty;                   //이름 초기화
        }
    }

    public List<JsonTest> rank = new List<JsonTest>();
    public void LoadScore()
    {
        rank.RemoveRange(0, rank.Count);                                        //리스트 초기화(전부 삭제)
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //파일개수 불러오기
        for (int i = 0; i < filecount; i++)                                     //.json 파일개수만큼 불러오기
        {
            string path = "TestJson/" + "Score" + i + ".json";
            string json = File.ReadAllText(path);
            JsonTest loadscore = JsonConvert.DeserializeObject<JsonTest>(json); //json 역직렬화
            rank.Add(loadscore);                                                //리스트에 JsonTest자료형 넣기

            Debug.Log($"test[{i}].Score = " + rank[i].Score);

        }
        JsonTest temp = new JsonTest(0, "");        //임시값
        for (int i = 0; i < filecount - 1; i++)     //오름차순 정렬
        {
            for (int j = i + 1; j < filecount; j++)
            {
                if (rank[i].Score < rank[j].Score)
                {
                    temp = rank[i];
                    rank[i] = rank[j];
                    rank[j] = temp;
                    OverWrite(i);
                    OverWrite(j);
                }
            }
        }
        testui.UpdateRanking();
        Debug.Log("FileCount : " + filecount);
    }

    public void OverWrite(int index)        //파일 오름차순으로 덮어쓰는 메소드(json파일 삭제하기 위해)
    {
        string filename = "Score" + index + ".json";            
        filename = Path.Combine("TestJson/", filename);
        string toJson = JsonConvert.SerializeObject(new JsonTest(rank[index].Score, rank[index].name), Formatting.Indented);
        File.WriteAllText(filename, toJson);
    }

    public void DeleteFile()        //파일이 5개 넘을 시 삭제
    {
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //파일개수 불러오기
        if (filecount > 5)
        {
            string path = "TestJson/" + "Score6.json";
            File.Delete(path);
        }
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
