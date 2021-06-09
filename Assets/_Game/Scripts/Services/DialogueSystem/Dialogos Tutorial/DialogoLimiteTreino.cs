using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoLimiteTreino : FluentScript
{
    public GameObject timeline;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    private bool inCombat = false;

    public override FluentNode Create()
    {
        return
            Yell(1.25f, "Dragg:\nVolta aqui, eu não tenho o dia todo! Você disse que queria treinar!") *
            Yell(1.25f, "Sieg:\n(É melhor aproveitar pra treinar enquanto o Dragg está na vila)");
    }

    public override void OnFinish()
    {
        if (!inCombat)
        {
            onFinish.Invoke();
        }
    }

    public override void OnStart()
    {
        if (!inCombat)
        {
            character.StopWalking();
            onStart.Invoke();
        }
    }

    public void SetInCombat()
    {
        inCombat = !inCombat;
    }
}