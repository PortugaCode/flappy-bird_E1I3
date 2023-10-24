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

    public void UpdateScore()
    {
        ScoreUI.text = string.Format("Score : {0}", GameManager.instance.score);
    }
}
