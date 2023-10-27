using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Stop()
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
    }
}
