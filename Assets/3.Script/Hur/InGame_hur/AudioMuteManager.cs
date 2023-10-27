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
        //�Ҹ��� ���� �ִ� �� ����Ʈ
        GameObject.Find("audio_btn").GetComponent<Image>().sprite = sprites[1];
    }
    public void OffSound()
    {
        AudioManager.Instance.StopBGM();
    }
    public void OnSound()
    {
        AudioManager.Instance.PlayBGM("Game");
    }
    public void BtnClick()
    {
        if (click)
        {
            click = false;
            OffSound();

            //�Ҹ��� �� �� = sprites[0]
            GameObject.Find("audio_btn").GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            click = true;
            OnSound();

            //�Ҹ� ų ��  = sprites[1]
            GameObject.Find("audio_btn").GetComponent<Image>().sprite = sprites[1];
        }
    }
    
}
