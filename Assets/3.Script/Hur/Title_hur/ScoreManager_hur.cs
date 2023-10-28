using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_hur : MonoBehaviour
{
    public Text[] Rank_txt;
    

    private int printCount;

    public void ShowRanking()
    {
        Debug.Log(GameManager.instance.rank.Count);
        printCount = GameManager.instance.rank.Count;
        if (printCount > 3) printCount = 3;         //printCount는 3등까지만
        for (int i = 0; i < printCount; i++)
        {
            Rank_txt[i].text = GameManager.instance.rank[i].Score.ToString();
        }

    }
}
