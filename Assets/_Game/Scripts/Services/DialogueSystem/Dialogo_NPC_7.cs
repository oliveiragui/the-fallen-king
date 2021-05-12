using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class Dialogo_NPC_7 : FluentScript
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
                    Write(0.5f, "Médico:\n...").WaitForButton()
                ) *
                If(() => timesVisited == 2,
                    Write(0.5f, "Médico:\nNão posso te ajudar agora.").WaitForButton()
                ) *
                If(() => timesVisited > 2,
                    Write(0.5f,
                            "Médico:\n(Essas pessoas estão morrendo de gripe? Lá no reino não vemos uma morte por gripe há décadas...)")
                        .WaitForButton() *
                    Write(0.5f, "Médico:\nPor favor, preciso focar nos doentes.").WaitForButton()
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