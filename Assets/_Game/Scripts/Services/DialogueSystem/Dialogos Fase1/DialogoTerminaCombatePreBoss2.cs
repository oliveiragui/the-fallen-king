using System;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoTerminaCombatePreBoss2 : FluentScript
{
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    public override FluentNode Create()
    {
        return
             Yell(3.0f, "Ótimo. E olha o tamanho dele...") *
             Yell(3.0f, "Quem são esses caras!?");
    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
        character.Stop();
        onStart.Invoke();
    }
}