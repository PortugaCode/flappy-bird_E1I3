using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum Animal
{
    first, second, third
}

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance = null;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
       
        //ΩÃ±€≈Ê
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        
    }

}
