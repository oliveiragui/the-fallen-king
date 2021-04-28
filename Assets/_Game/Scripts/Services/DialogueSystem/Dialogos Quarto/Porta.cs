using System;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class Porta : FluentScript
{
    public GameObject timeline;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    [SerializeField] UnityEvent changeScene;


    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.25f, "Sair da casa?").WaitForButton() *
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
        character.Stop();
        onStart.Invoke();
    }

    public void ChangeScene()
    {
        changeScene.Invoke();
    }
}
