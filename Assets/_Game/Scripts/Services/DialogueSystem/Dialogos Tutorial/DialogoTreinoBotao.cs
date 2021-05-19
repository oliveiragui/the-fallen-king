﻿using Fluent;
using System.Collections;
using System.Collections.Generic;
using _Game.GameModules.Entities.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DialogoTreinoBotao : FluentScript
{
    int timesVisited = 0;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    [SerializeField] UnityEvent onFinishTwoTimes;

    public override FluentNode Create()
    {
        return
            Show() *
            Do(() => timesVisited++) *
            If(() => timesVisited == 1,
                Write(0.5f, "Dragg:\nVolte a falar comigo quando estiver pronto pro treino!").WaitForButton()
            ) *
            If(() => timesVisited >= 2,
                Write(0.5f, "Dragg:\nPara atacar aperte <sprite=\"KeyBoardAndMouse\" name=\"Mouse_Left_Key_Dark\"> ").WaitForButton() *
                Write(0.5f, "Dragg:\nPara se esquivar aperte <sprite=\"KeyBoardAndMouse\" name=\"Space_Key_Dark\"> ").WaitForButton() 
            ) *
            Hide();
    }

    public override void OnFinish()
    {
        if (timesVisited >= 2) 
            onFinish.Invoke();
        else
            onFinishTwoTimes.Invoke();
    }

    public override void OnStart() 
    {
        character.StopWalking();
        onStart.Invoke();
    }
}