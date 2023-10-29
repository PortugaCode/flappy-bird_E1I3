using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBtnEvent : MonoBehaviour
{
    public void Exit_game()
    {
        Application.Quit();
    }
/*    public void Editor_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application Quit();
#endif

    }*/
}
