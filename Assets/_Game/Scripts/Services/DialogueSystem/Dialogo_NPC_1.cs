﻿using System;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo_NPC_1 : FluentScript
{
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return 
            Show() *
            Write(0.5f, "Mulher:\nA nossa vila é bem pacífica, mas por ser em uma região montanhosa, fica bem difícil das coisas chegarem aqui.").WaitForButton() *
            Write(0.5f, "Homem:\nMe pergunto se não é melhor assim... Embora nos faltem muitas coisas...").WaitForButton() *
            Write(0.5f, "Homem:\nAquele médico por exemplo, seus métodos de tratamento são muito avançados, ele fez o estoque de remédios durarem o dobro de tempo.").WaitForButton() *
            Write(0.5f, "Mulher:\nDe onde será que ele veio? Desde que ele chegou, nós não temos muitos enfermos. ").WaitForButton() *
            Write(0.5f, "Mulher:\nMeu pai mesmo foi curado e já até voltou a trabalhar na marcenaria, graças a ele.").WaitForButton() *
            Write(0.5f, "Homem:\nDe onde será...").WaitForButton() *
            Hide();

    }
    // void OnTriggerExit(Collider collider)
    // {
    // }

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
