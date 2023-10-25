using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControll_third : MonoBehaviour
{
    private Animator anim;
    private CharacterSelection char_select;
    private void Start()
    {
        anim = GetComponent<Animator>();
        char_select = FindObjectOfType<CharacterSelection>();
    }
    private void Update()
    {
        Ready();
    }
    private void Ready()
    {
        if (char_select.ready)
        {
            anim.SetTrigger("Gogame2");
        }
    }
}
