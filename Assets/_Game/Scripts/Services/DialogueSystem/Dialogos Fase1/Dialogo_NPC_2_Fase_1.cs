using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo_NPC_2_Fase_1 : FluentScript
{
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Padre:\nDeus os abençoe na sua viagem...").WaitForButton() *
            Write(0.5f, "Padre:\nO Pirin fez muitas coisas boas pela nossa vila, eu oro para que vocês consigam retornar vitoriosos...").WaitForButton() *
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