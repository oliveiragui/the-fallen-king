using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class PortaEntrada : FluentScript
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
            Write(0.25f, "Entrar em casa?").WaitForButton() *
            Options
            (
                Option("Sim") *
                    Hide() *
                    Do(() => AtivaGameObject(timeline)) *
                    Do(() => ChangeScene()) *
                    End() *

                Option("Não") *
                    Hide() *
                    End()
             );
    }

    private void AtivaGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);

        }
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
