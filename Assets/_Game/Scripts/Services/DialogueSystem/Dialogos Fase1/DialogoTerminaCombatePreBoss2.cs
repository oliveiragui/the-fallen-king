using System;
using _Game.GameModules.Entities.Scripts;
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
             Yell(3.0f, "Sieg:\nÓtimo. E olha o tamanho dele...") *
             Yell(3.0f, "Sieg:\nQuem são esses caras!?");
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