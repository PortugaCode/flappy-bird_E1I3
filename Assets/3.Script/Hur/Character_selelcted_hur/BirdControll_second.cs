using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControll_second : MonoBehaviour
{
    private Animator anim;
    private CharacterManager char_manager;
    private void Start()
    {
        anim = GetComponent<Animator>();
        char_manager = FindObjectOfType<CharacterManager>();
    }
    private void Update()
    {
        Ready();
    }
    private void Ready()
    {
        if (char_manager.ready)
        {
            anim.SetTrigger("Gogame1");
        }
    }
}
