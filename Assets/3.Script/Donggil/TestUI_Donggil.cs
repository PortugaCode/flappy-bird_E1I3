using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI_Donggil : MonoBehaviour
{
    public Text ScoreUI;
    public Text RankingUI;
    public InputField input;
    public string playerName = string.Empty;

    [SerializeField] private int outputRank;

    public void UpdateScore()
    {
        ScoreUI.text = string.Format("Score : {0}", GameManager.instance.current_score);
    }

    public void UpdateRanking()
    {
        RankingUI.text = string.Empty;
        outputRank = GameManager.instance.rank.Count;
        if (outputRank > 3) outputRank = 3;
        for (int i = 0; i < outputRank; i++)
        {
            RankingUI.text += string.Format("{0}µî : {1} : " + GameManager.instance.rank[i].name + "\n\n\n", i + 1, GameManager.instance.rank[i].Score);

        }
    }

    public void InputName()
    {
        if (input.text != string.Empty)
        {
            playerName = input.text;
            GameManager.instance.current_name = playerName;
            Debug.Log(GameManager.instance.current_name);
            input.text = string.Empty;
        }
    }
}
