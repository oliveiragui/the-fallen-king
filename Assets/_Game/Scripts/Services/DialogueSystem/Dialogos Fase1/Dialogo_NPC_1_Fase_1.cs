using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo_NPC_1_Fase_1 : FluentScript
{
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Homem:\nQuerida, o médico disse que vai ficar tudo bem, descanse o quanto puder").WaitForButton() *
            Write(0.5f, "Mulher:\nCof Cof Cof").WaitForButton() *
            Write(0.5f, "Homem:\nEu não sei o que eu faria sem você...").WaitForButton() *
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