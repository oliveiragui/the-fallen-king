using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo_NPC_3_Fase_1 : FluentScript
{
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Mulher:\nNossas casas, nossos pertences, quanta crueldade esse exército é capaz de fazer?").WaitForButton() *
            Write(0.5f, "Mulher:\nGraças a Deus o médico conseguiu tratar todos os feridos...").WaitForButton() *
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