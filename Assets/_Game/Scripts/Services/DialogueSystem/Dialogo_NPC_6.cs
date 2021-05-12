using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class Dialogo_NPC_6 : FluentScript
    {
        [SerializeField] Entity character;
        [SerializeField] UnityEvent onStart;
        [SerializeField] UnityEvent onFinish;
        int timesVisited = 0;

        public override FluentNode Create() =>
            Show() *
            Write(0.5f, "Mulher:\nOh meu bem, nós encontramos alguém que poderia ter te curado.").WaitForButton() *
            Write(0.5f, "Mulher:\nAh, se ao menos você visse como estamos melhores...").WaitForButton() *
            Write(0.5f, "Vendedor:\nEu tenho tanta saudades...").WaitForButton() *
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