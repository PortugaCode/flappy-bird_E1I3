using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_hur : MonoBehaviour
{
    private int currentScore = 0;
    

    private int highscore_1;
    private int highscore_2;
    private int highscore_3;
    private int lastscore;

    public Text first_txt;
    public Text second_txt;
    public Text third_txt;
    public Text LastScore_txt;

    private void Start()
    {
        highscore_1 = PlayerPrefs.GetInt("1st");
        highscore_2 = PlayerPrefs.GetInt("2nd");
        highscore_3 = PlayerPrefs.GetInt("3rd");

        //빌드 할 때 0으로 초기화
        //PlayerPrefs.SetInt("1st", 0);
        //PlayerPrefs.SetInt("2nd", 0);
        //PlayerPrefs.SetInt("3rd", 0);
        //PlayerPrefs.GetInt("last", 0);
    }

    private void Update()
    {
        SaveScore(); //tilte 씬의 랭킹판에 저장되는 점수
    }
    public void UpdateHighScore()
    {
        if(currentScore > highscore_1)
        {
            PlayerPrefs.SetInt("2nd", highscore_2);
            PlayerPrefs.SetInt("3rd", highscore_3);
            PlayerPrefs.SetInt("1st", currentScore);
        }
        else if (currentScore > highscore_2)
        {
            PlayerPrefs.SetInt("3rd", highscore_2);
            PlayerPrefs.SetInt("2nd", currentScore);
        }
        else if (currentScore > highscore_3)
        {
            PlayerPrefs.SetInt("3rd", currentScore);
        }
    }
    public void Gameover()
    {
        if (true) //플레이어가 죽었을 때
        {
            UpdateHighScore();
        }
    }
    public void SaveScore()
    {
        if (true) //게임 오버일 때
        {
            LastScore_txt.text = $"{PlayerPrefs.GetInt($"Last score : {lastscore}")}";
        }

        if (true) //타이틀 씬에서 랭킹 버튼 누를 때
        {
            first_txt.text = $"{PlayerPrefs.GetInt("1st")}";
            second_txt.text = $"{PlayerPrefs.GetInt("2nd")}";
            third_txt.text = $"{PlayerPrefs.GetInt("3rd")}";
        }
    }

}
