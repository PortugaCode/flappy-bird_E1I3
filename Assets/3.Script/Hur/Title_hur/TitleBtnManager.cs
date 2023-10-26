using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtnManager : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Open_Option()
    {
        anim.SetTrigger("option_open");
    }
    public void Close_Option()
    {

    }
}
