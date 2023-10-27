using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_hur : MonoBehaviour
{
    public Text first_txt;
    public Text second_txt;
    public Text third_txt;
   
    public void ShowRanking()
    {
        Debug.Log(GameManager.instance.rank.Count);

        first_txt.text = GameManager.instance.rank[0].Score.ToString();
        second_txt.text = GameManager.instance.rank[1].Score.ToString();
        third_txt.text = GameManager.instance.rank[2].Score.ToString();
    }
}
