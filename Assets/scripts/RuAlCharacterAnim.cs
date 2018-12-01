﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuAlCharacterAnim : MonoBehaviour {


    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
           

        }
        else
        {
            anim.SetBool("isRunning", false);
            
        }
       

    }
}
