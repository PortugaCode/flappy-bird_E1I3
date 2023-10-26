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
        isGameOver = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            isGameOver = true;
            SaveName();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadScore();

            DeleteFile();
            testui.UpdateRanking();
            isGameOver = false;
        }


    }

    public int current_score = 0;
    public string current_name = string.Empty;


    public void Score(int s)
    {
        current_score += s;
        testui.UpdateScore();
    }


    private void SaveName()
    {
        if (isGameOver)
        {
            StartCoroutine(GameOverInputName());
        }
    }

    private IEnumerator GameOverInputName()
    {
        yield return new WaitForSeconds(3f);            //3�� �ڿ�
        testui.input.gameObject.SetActive(true);        //InputField �ѱ�
        testui.InputName();                             //�̸� �Է�
    }

    private void SaveScore()
    {
        if (isGameOver)
        {
            int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //���ϰ��� �ҷ�����
            if (current_name != string.Empty)
            {
                string filename = "Score" + filecount + ".json";                    //���� �̸��� Score��ȣ.json
                filename = Path.Combine("TestJson/", filename);
                string toJson = JsonConvert.SerializeObject(new RankInfo(current_score, current_name), Formatting.Indented);        //json ����ȭ

                File.WriteAllText(filename, toJson);            //���� ����
                Debug.Log("Save " + filename);
                current_name = string.Empty;                   //�̸� �ʱ�ȭ
            }
        }
    }

    public List<RankInfo> rank = new List<RankInfo>();
    public void LoadScore()
    {
        rank.RemoveRange(0, rank.Count);                                        //����Ʈ �ʱ�ȭ(���� ����)
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //���ϰ��� �ҷ�����
        for (int i = 0; i < filecount; i++)                                     //.json ���ϰ�����ŭ �ҷ�����
        {
            string rankFileName = "Score" + i + ".json";
            rankFileName = Path.Combine("TestJson/", rankFileName);                  //���
            string json = File.ReadAllText(rankFileName);
            RankInfo loadscore = JsonConvert.DeserializeObject<RankInfo>(json); //json ������ȭ
            rank.Add(loadscore);                                                //����Ʈ�� RankInfo�ڷ��� �ֱ�

            Debug.Log($"test[{i}].Score = " + rank[i].Score);

        }
        RankInfo temp = new RankInfo(0, "");        //�ӽð�
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
        Debug.Log("FileCount : " + filecount);
    }

    private void OverWrite(int index)        //���� ������������ ����� �޼ҵ�(json���� �����ϱ� ����)
    {
        string filename = "Score" + index + ".json";
        filename = Path.Combine("TestJson/", filename);
        string toJson = JsonConvert.SerializeObject(new RankInfo(rank[index].Score, rank[index].name), Formatting.Indented);
        File.WriteAllText(filename, toJson);
    }

    private void DeleteFile()        //������ 5�� ���� �� ����
    {
        int filecount = Directory.GetFiles("TestJson/", "*.json").Length;       //���ϰ��� �ҷ�����
        if (filecount > 5)
        {
            string path = "TestJson/" + "Score5.json";
            File.Delete(path);
        }
    }
}
[System.Serializable]
public class RankInfo
{
    public int Score;
    public string name;

    public RankInfo(int score, string name)
    {
        Score = score;
        this.name = name;
    }
}
