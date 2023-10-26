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
        
        testui.input.gameObject.SetActive(true);        //InputField �ѱ�
        testui.InputName();                             //�̸� �Է�
    }

    public void SaveScore()
    {
        
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //���ϰ��� �ҷ�����
        if (current_name != string.Empty)
        {
            string filename = "Score" + filecount + ".json";                    //���� �̸��� Score��ȣ.json
            filename = Path.Combine("TestJson/", filename);
            string toJson = JsonConvert.SerializeObject(new JsonTest(current_score, current_name), Formatting.Indented);        //json ����ȭ

            File.WriteAllText(filename, toJson);            //���� ����
            Debug.Log("Save " + filename);
            current_name = string.Empty;                   //�̸� �ʱ�ȭ
        }
    }

    public List<JsonTest> rank = new List<JsonTest>();
    public void LoadScore()
    {
        rank.RemoveRange(0, rank.Count);                                        //����Ʈ �ʱ�ȭ(���� ����)
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //���ϰ��� �ҷ�����
        for (int i = 0; i < filecount; i++)                                     //.json ���ϰ�����ŭ �ҷ�����
        {
            string path = "TestJson/" + "Score" + i + ".json";
            string json = File.ReadAllText(path);
            JsonTest loadscore = JsonConvert.DeserializeObject<JsonTest>(json); //json ������ȭ
            rank.Add(loadscore);                                                //����Ʈ�� JsonTest�ڷ��� �ֱ�

            Debug.Log($"test[{i}].Score = " + rank[i].Score);

        }
        JsonTest temp = new JsonTest(0, "");        //�ӽð�
        for (int i = 0; i < filecount - 1; i++)     //�������� ����
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

    public void OverWrite(int index)        //���� ������������ ����� �޼ҵ�(json���� �����ϱ� ����)
    {
        string filename = "Score" + index + ".json";            
        filename = Path.Combine("TestJson/", filename);
        string toJson = JsonConvert.SerializeObject(new JsonTest(rank[index].Score, rank[index].name), Formatting.Indented);
        File.WriteAllText(filename, toJson);
    }

    public void DeleteFile()        //������ 5�� ���� �� ����
    {
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //���ϰ��� �ҷ�����
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
