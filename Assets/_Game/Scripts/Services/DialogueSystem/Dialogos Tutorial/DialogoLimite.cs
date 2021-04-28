using System;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoLimite : FluentScript
{
    public GameObject timeline;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Yell(1.25f, "Melhor não.");
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

    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            timeline.SetActive(true);
        }
    }
}
