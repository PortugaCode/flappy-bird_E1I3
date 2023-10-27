using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMuteManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private Sprite[] sprites;
    
    private bool click = true;
    private void Start()
    {
        bgm = GetComponent<AudioSource>();

        //소리가 켜져 있는 게 디폴트
        GameObject.Find("audio_btn").GetComponent<Image>().sprite = sprites[1];
    }
    public void OffSound()
    {
       bgm.Stop();
    }
    public void OnSound()
    {
       bgm.Play();
    }
    public void BtnClick()
    {
        if (click)
        {
            click = false;
            OffSound();

            //소리를 끌 때 = sprites[0]
            GameObject.Find("audio_btn").GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            click = true;
            OnSound();

            //소리 킬 때  = sprites[1]
            GameObject.Find("audio_btn").GetComponent<Image>().sprite = sprites[1];
        }
    }
    
}
