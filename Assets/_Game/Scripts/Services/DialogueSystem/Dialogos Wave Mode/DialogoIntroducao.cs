using System;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem.Dialogos_Wave_Mode
{
    public class DialogoIntroducao : FluentScript
    {
        public GameObject timeline;
        public UnityEvent onFinish;

        public override FluentNode Create()
        {
            return
                Show() *
                Write("Sieg:\nEles destruiram a vida do meu povo, nos enterraram nas cinzas do que tinhamos...").WaitForButton() *
                Write("Sieg:\nE Preciso parar-los antes que destruam outros povos").WaitForButton() *
                Do(() => onFinish.Invoke()) *
                Hide();
        }
    }
}