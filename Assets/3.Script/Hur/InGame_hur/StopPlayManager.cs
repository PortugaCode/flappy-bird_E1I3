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
        Debug.Log("»≠∏È ∏ÿ√Á!!");
    }
    private void Play_game()
    {
        Debug.Log("«√∑π¿Ã¡ﬂ...");
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
