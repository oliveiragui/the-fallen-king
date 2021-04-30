﻿using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class Dialogo_NPC_3 : FluentScript
    {
        [SerializeField] Entity character;
        [SerializeField] UnityEvent onStart;
        [SerializeField] UnityEvent onFinish;
        int timesVisited;

        public override FluentNode Create()
        {
            return
                Show() *
                Do(() => timesVisited++) *
                If(() => timesVisited == 1,
                    Write(0.5f, "Vendedor:\nJovem, não tenho mais nada que possa te vender, volte amanhã.").WaitForButton()
                ) *
                If(() => timesVisited == 2,
                    Write(0.5f, "Vendedor:\nAh, não liga para aquele ranzinza ali na loja da frente. É o meu irmão.")
                        .WaitForButton() *
                    Write(0.5f, "Vendedor:\nEle odeia ter que parar de trabalhar por causa do estoque...").WaitForButton() *
                    Write(0.5f,
                            "Vendedor:\nMal sabe ele o quão tranquila é nossa vida aqui na vila, nunca fomos roubados nem nada...")
                        .WaitForButton()
                ) *
                If(() => timesVisited > 2,
                    Write(0.5f, "Vendedor:\nÉ, essa tranquilidade eu não troco por nada...").WaitForButton()
                ) *
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
}