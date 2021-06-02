using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoPlayerMorre : FluentScript
{
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Yell(4.5f, "General:\nHAR HAR HAR HAR. E eu achando que iria me divertir um pouco mais!");
    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
        onStart.Invoke();
    }
}