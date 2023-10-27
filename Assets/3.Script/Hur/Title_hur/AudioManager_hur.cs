using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_hur : MonoBehaviour
{
    public new AudioSource audio;

    public void SetAudioVolume(float volume)
    {
        audio.volume = volume;
    }
}
