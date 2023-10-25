using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI_Donggil : MonoBehaviour
{
    public static TestUI_Donggil instance = null;

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

    public Text ScoreUI;
    public Text RankingUI;

    public void UpdateScore()
    {
        ScoreUI.text = string.Format("Score : {0}", GameManager.instance.current_score);
    }

    public void UpdateRanking(int index)
    {
        RankingUI.text = string.Format("{0}µî : {1}" + GameManager.instance.test[index].name + "\n", index, GameManager.instance.test[index].Score);
    }
}
