using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using _Game.Scripts.GameContent.Entities;

public class Dialogo_NPC_5 : FluentScript
{
    int timesVisited = 0;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {

        return
            Show() *
            Write(0.5f, "Padre:\nMinhas preces foram atendidas... Ninguém mais precisa morrer doente...").WaitForButton() *
            Write(0.5f, "Padre:\nMeu Deus, finalmente há cura na nossa vila...").WaitForButton() *
            Write(0.5f, "Vendedor:\nÉ um milagre... esse médico estar aqui... só pode ser um milagre...").WaitForButton() *
            Hide();

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
}
