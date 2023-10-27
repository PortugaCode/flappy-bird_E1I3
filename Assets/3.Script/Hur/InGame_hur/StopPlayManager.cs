using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopPlayManager : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private bool click = true;
    private void Start()
    {
        GameObject.Find("Stop_btn").GetComponent<Image>().sprite = sprites[1];
        Play_game();
    }
    private void Stop_game()
    {
        Time.timeScale = 0;
    }
    private void Play_game()
    {
        Time.timeScale = 1.0f;
    }
    public void BtnClick()
    {
        if (click)
        {
            click = false;
            Stop_game();
            GameObject.Find("Stop_btn").GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            click = true;
            Play_game();
            GameObject.Find("Stop_btn").GetComponent<Image>().sprite = sprites[1];
        }
    }
}
