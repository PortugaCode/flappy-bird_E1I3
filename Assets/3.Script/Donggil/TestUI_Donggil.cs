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

    public void UpdateScore()
    {
        ScoreUI.text = string.Format("Score : {0}", GameManager.instance.current_score);
    }

    public void UpdateRanking()
    {
        RankingUI.text = string.Empty;
        for (int i = 0; i < 3; i++)
        {
            RankingUI.text += string.Format("{0}µî : {1} : " + GameManager.instance.test[i].name + "\n\n\n", i + 1, GameManager.instance.test[i].Score);
        }
    }

    public void InputName(int index)
    {
        input.gameObject.SetActive(true);
        playerName = input.text;
        GameManager.instance.current_name = playerName;
        input.gameObject.SetActive(false);

    }
}
