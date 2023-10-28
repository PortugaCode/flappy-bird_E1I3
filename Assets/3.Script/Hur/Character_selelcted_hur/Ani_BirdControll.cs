using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ani_BirdControll : MonoBehaviour
{
    private Animator anim;
    private CharacterManager char_manager;
    private void Start()
    {
        anim = GetComponent<Animator>();
        char_manager = FindObjectOfType<CharacterManager>();
    }
    //private void Update()
    //{
    //    Ready();
    //}
    public void Ready()
    {
        if (char_manager.ready)
        {
            anim.SetTrigger("Gogame");
        }
    }
}
