using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class IniciaFogareu : FluentScript
{
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    [SerializeField] UnityEvent changeScene;


    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.25f, "Chamar inimigos para o combate?").WaitForButton() *
            Options
            (
                Option("Sim") *
                Hide() *
                Do(() => ChangeScene()) *
                End() *

                Option("Não") *
                Hide() *
                End()
            );
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

    public void ChangeScene()
    {
        changeScene.Invoke();
    }
}