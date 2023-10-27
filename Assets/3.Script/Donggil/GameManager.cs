using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;


/*
 Ctrl + F 로 주석 찾기
 +++ : 추가
 *** : 바꾸기
 ### : 메소드 넣기
 
 */
public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance = null;

    private void Awake()
    {
        //Time.timeScale = 0f;
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

    //private UIUpdate uiUpdate;      //***UI스크립트 바뀌면 여기 바꾸시오***
    public bool isGameOver = false;
    public bool isGameStart = false;

    public int current_score = 0;
    public string current_name = string.Empty;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        //uiUpdate = FindObjectOfType<UIUpdate>();    //***UI스크립트 바뀌면 여기 바꾸시오***
        isGameStart = false;
        isGameOver = false;
    }

    public void Init_MainToMain()
    {
        Debug.Log("MTM");

        // main -> main 는 괜찮

        //gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
        //scoreText = gameOverUI.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();

        current_score = 0;
        isGameOver = false;
        isGameStart = true;
    }

    public void Init_TitleToMain()
    {
        Debug.Log("TTM");

        // title -> main 일때 null

        current_score = 0;
        isGameOver = false;
        isGameStart = true;
    }

    public void GameStart()
    {
        if (!isGameStart)               //게임 시작 되면
        {
            if (Input.anyKeyDown)       //아무 키 입력 시
            {
                Time.timeScale = 1f;    //시간 재생
                isGameStart = true;     //게임스타트
            }
        }
    }


    public void Score(int s)        //***점수 메소드 바꿀시 여기***
    {
        current_score += s;
        //uiUpdate.UpdateScore();     //***점수 UI 바꿀시 여기***
    }

    public void GameOver()          //###게임오버 될 시 이 메소드 넣기###
    {
        Debug.Log("GameOver호출");
        isGameOver = true;
        StartCoroutine(GameOverInputName());
    }

    private IEnumerator GameOverInputName()
    {
        gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
        scoreText = gameOverUI.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();

        yield return new WaitForSeconds(1f);            //1초 뒤에...
        Time.timeScale = 0;                             //시간 재생 0
        //나중에 바꿀 InputField UI
        //***UI스크립트 바뀌면 여기 바꾸시오***
        //uiUpdate.input.gameObject.SetActive(true);        //InputField 켜기
        //uiUpdate.InputName();                             //이름 입력

        scoreText.text = current_score.ToString();
        gameOverUI.transform.GetChild(0).gameObject.SetActive(true);

        SaveScore();
        LoadScore();
    }


    #region json

    public List<RankInfo> rank = new List<RankInfo>();
    private void SaveScore()        //###이름 입력(InputField) 자식 버튼event에 이 메소드 넣기###
    {
        int filecount = Directory.GetFiles("RankInfoFile/", "*.json").Length;       //파일개수 불러오기

        string filename = "Score" + filecount + ".json";                    //파일 이름은 Score번호.json
        filename = Path.Combine("RankInfoFile/", filename);
        string toJson = JsonConvert.SerializeObject(new RankInfo(current_score), Formatting.Indented);        //json 직렬화

        File.WriteAllText(filename, toJson);            //파일 쓰기
        Debug.Log("Save " + filename);
        current_name = string.Empty;                   //이름 초기화

        //Time.timeScale = 1f;                                //***이름 다 입력되면 다시 시간 재생인데.. 필요없을 시 빼기***
        //+++타이틀 씬 로드 추가하기+++

    }

    public void LoadScore()         //###랭킹 보기 버튼에 이 메서드 넣기###
    {
        rank.Clear();                                                           //리스트 초기화(전부 삭제)
        int filecount = Directory.GetFiles("RankInfoFile/", "*.json").Length;   //파일개수 불러오기
        for (int i = 0; i < filecount; i++)                                     //.json 파일개수만큼 불러오기
        {
            string rankFileName = "Score" + i + ".json";
            rankFileName = Path.Combine("RankInfoFile/", rankFileName);                  //경로
            string json = File.ReadAllText(rankFileName);
            RankInfo loadscore = JsonConvert.DeserializeObject<RankInfo>(json); //json 역직렬화
            rank.Add(loadscore);                                                //리스트에 RankInfo자료형 넣기

            Debug.Log($"test[{i}].Score = " + rank[i].Score);
        }

        RankInfo temp = new RankInfo(0);        //임시값
        for (int i = 0; i < filecount - 1; i++)     //오름차순 정렬
        {
            for (int j = i + 1; j < filecount; j++)
            {
                if (rank[i].Score < rank[j].Score)
                {
                    temp = rank[i];
                    rank[i] = rank[j];
                    rank[j] = temp;
                    OverWrite(i);       //json덮어쓰기(이유는 json파일 5개 유지하기 위해)
                    OverWrite(j);
                }
            }
        }
        DeleteFile();
        Debug.Log("FileCount : " + filecount);
    }

    private void OverWrite(int index)        //파일 오름차순으로 덮어쓰는 메소드(json파일 삭제하기 위해)
    {
        string filename = "Score" + index + ".json";
        filename = Path.Combine("RankInfoFile/", filename);
        string toJson = JsonConvert.SerializeObject(new RankInfo(rank[index].Score), Formatting.Indented);
        File.WriteAllText(filename, toJson);
    }

    private void DeleteFile()        //파일이 5개 넘을 시 삭제
    {
        int filecount = Directory.GetFiles("RankInfoFile/", "*.json").Length;       //파일개수 불러오기
        if (filecount > 4)
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

    public RankInfo(int score)
    {
        Score = score;
    }
}
