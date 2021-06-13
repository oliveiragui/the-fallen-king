using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class Cama : FluentScript
{
    public GameObject timeline;
    public GameObject talkText;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.25f, "Você se sente mais cansado que o normal.").WaitForButton() *
            Write(0.25f, "Sieg:\nAcho melhor eu dormir...").WaitForButton() *
            Do(() => IniciaCutscene()) *
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

    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            talkText.SetActive(false);
            timeline.SetActive(true);
        }
    }
}
