using Fluent;
using System.Collections;
using System.Collections.Generic;
using _Game.GameModules.Entities.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo_NPC_6 : FluentScript
{
    int timesVisited = 0;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {

        return
            Show() *
            Write(0.5f, "Mulher:\nOh meu bem, nós encontramos alguém que poderia ter te curado.").WaitForButton() *
            Write(0.5f, "Mulher:\nAh, se ao menos você visse como estamos melhores...").WaitForButton() *
            Write(0.5f, "Mulher:\nEu tenho tanta saudades...").WaitForButton() *
            Hide();

    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
        character.StopWalking();
        onStart.Invoke();
    }
}
