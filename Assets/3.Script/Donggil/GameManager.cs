using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


/*
 Ctrl + F �� �ּ� ã��
 +++ : �߰�
 *** : �ٲٱ�
 ### : �޼ҵ� �ֱ�
 
 */
public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance = null;

    private void Awake()
    {
        Time.timeScale = 0f;
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

    #endregion

    private UIUpdate uiUpdate;      //***UI��ũ��Ʈ �ٲ�� ���� �ٲٽÿ�***
    public bool isGameOver = false;
    public bool isGameStart = false;

    public int current_score = 0;
    public string current_name = string.Empty;



    private void Start()
    {
        uiUpdate = FindObjectOfType<UIUpdate>();    //***UI��ũ��Ʈ �ٲ�� ���� �ٲٽÿ�***
        isGameStart = false;
        isGameOver = false;
    }

    private void Update()
    {
        GameStart();
    }

    public void GameStart()
    {
        if (!isGameStart)               //���� ���� �Ǹ�
        {
            if (Input.anyKeyDown)       //�ƹ� Ű �Է� ��
            {
                Time.timeScale = 1f;    //�ð� ���
                isGameStart = true;     //���ӽ�ŸƮ
            }
        }
    }


    public void Score(int s)        //***���� �޼ҵ� �ٲܽ� ����***
    {
        current_score += s;
        uiUpdate.UpdateScore();     //***���� UI �ٲܽ� ����***
    }

    public void GameOver()          //###���ӿ��� �� �� �� �޼ҵ� �ֱ�###
    {
        isGameOver = true;
        StartCoroutine(GameOverInputName());
    }

    private IEnumerator GameOverInputName()
    {
        yield return new WaitForSeconds(1f);            //3�� �ڿ�...
        Time.timeScale = 0;                             //�ð� ��� 0
        //���߿� �ٲ� InputField UI
        //***UI��ũ��Ʈ �ٲ�� ���� �ٲٽÿ�***
        uiUpdate.input.gameObject.SetActive(true);        //InputField �ѱ�
        uiUpdate.InputName();                             //�̸� �Է�
    }

    
    #region json
    
    public List<RankInfo> rank = new List<RankInfo>();
    private void SaveScore()        //###�̸� �Է�(InputField) �ڽ� ��ưevent�� �� �޼ҵ� �ֱ�###
    {
        int filecount = Directory.GetFiles("RankInfoFile/", "*.json").Length;       //���ϰ��� �ҷ�����
        if (current_name != string.Empty)
        {
            string filename = "Score" + filecount + ".json";                    //���� �̸��� Score��ȣ.json
            filename = Path.Combine("RankInfoFile/", filename);
            string toJson = JsonConvert.SerializeObject(new RankInfo(current_score, current_name), Formatting.Indented);        //json ����ȭ

            File.WriteAllText(filename, toJson);            //���� ����
            Debug.Log("Save " + filename);
            current_name = string.Empty;                   //�̸� �ʱ�ȭ
        }
        //Time.timeScale = 1f;                                //***�̸� �� �ԷµǸ� �ٽ� �ð� ����ε�.. �ʿ���� �� ����***
        //+++Ÿ��Ʋ �� �ε� �߰��ϱ�+++

    }

    public void LoadScore()         //###��ŷ ���� ��ư�� �� �޼��� �ֱ�###
    {
        rank.RemoveRange(0, rank.Count);                                        //����Ʈ �ʱ�ȭ(���� ����)
        int filecount = Directory.GetFiles("RankInfoFile/", "*.json").Length;       //���ϰ��� �ҷ�����
        for (int i = 0; i < filecount; i++)                                     //.json ���ϰ�����ŭ �ҷ�����
        {
            string rankFileName = "Score" + i + ".json";
            rankFileName = Path.Combine("RankInfoFile/", rankFileName);                  //���
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
                    OverWrite(i);       //json�����(������ json���� 5�� �����ϱ� ����)
                    OverWrite(j);
                }
            }
        }
        uiUpdate.UpdateRanking();    //***UI��ũ��Ʈ �ٲ�� ���� �ٲٽÿ�***
        DeleteFile();
        Debug.Log("FileCount : " + filecount);
    }

    private void OverWrite(int index)        //���� ������������ ����� �޼ҵ�(json���� �����ϱ� ����)
    {
        string filename = "Score" + index + ".json";
        filename = Path.Combine("RankInfoFile/", filename);
        string toJson = JsonConvert.SerializeObject(new RankInfo(rank[index].Score, rank[index].name), Formatting.Indented);
        File.WriteAllText(filename, toJson);
    }

    private void DeleteFile()        //������ 5�� ���� �� ����
    {
        int filecount = Directory.GetFiles("RankInfoFile/", "*.json").Length;       //���ϰ��� �ҷ�����
        if (filecount > 5)
        {
            string path = "RankInfoFile/" + "Score5.json";
            File.Delete(path);
        }
    }
    #endregion
    
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
