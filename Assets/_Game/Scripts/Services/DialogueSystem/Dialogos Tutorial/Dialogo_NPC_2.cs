using Fluent;
using System.Collections;
using System.Collections.Generic;
using _Game.GameModules.Entities.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo_NPC_2 : FluentScript
{
    int timesVisited = 0;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Beto (Vendedor):\nDo que você precisa, rapaz? O estoque de hoje já foi.").WaitForButton() * 
            Write(0.5f, "Beto (Vendedor):\nO rapaz que traz nossos itens já deve estar fora da vila a essa altura. Volte amanhã.").WaitForButton() *
            Write(0.5f, "Beto (Vendedor):\nArgh... Por que tudo tem que ser longe da vila...").WaitForButton() *
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
