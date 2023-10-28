using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBtnManager : MonoBehaviour
{
    public void SceneLoad(string name)
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(name);

        if (SceneManager.GetActiveScene().name == "CharacterSelected_hur")
        {
            GameManager.instance.Init_TitleToMain();
        }
        else if (SceneManager.GetActiveScene().name == "Title_hur")
        {
            return;
        }
        else
        {
            GameManager.instance.Init_MainToMain();
        }

    }
}
