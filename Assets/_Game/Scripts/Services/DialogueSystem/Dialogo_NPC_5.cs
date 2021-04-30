using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class Dialogo_NPC_5 : FluentScript
    {
        [SerializeField] Entity character;
        [SerializeField] UnityEvent onStart;
        [SerializeField] UnityEvent onFinish;
        int timesVisited = 0;

        public override FluentNode Create() =>
            Show() *
            Write(0.5f, "Padre:\nMinhas preces foram atendidas... Ninguém mais precisa morrer doente...").WaitForButton() *
            Write(0.5f, "Padre:\nMeu Deus, finalmente há cura na nossa vila...").WaitForButton() *
            Write(0.5f, "Vendedor:\nÉ um milagre... esse médico estar aqui... só pode ser um milagre...").WaitForButton() *
            Hide();

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