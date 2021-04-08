﻿using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConversaTeste : FluentScript
{
    public GameObject timeline;
    public GameObject player = null;
    //public GameObject playerAnimation = null;


    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Lorem:\n Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essen...!") *
            Options
            (
                Option("") *
                    Hide() *
                    //Do(() => IniciaCutscene()) *
                    End()
             );

    }

    public override void OnFinish() 
    {
    }

    public override void OnStart() 
    { 
    }

    private void IniciaCutscene()
    {
        timeline.SetActive(true);

        //if (player != null && playerAnimation != null)
        //{
        //    timeline.get
        //}
    }
}