using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class Dialogo_NPC_2 : FluentScript
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
                    Write(0.5f, "Vendedor:\nDo que você precisa, rapaz? O estoque de hoje já foi.").WaitForButton()
                ) *
                If(() => timesVisited == 2,
                    Write(0.5f,
                            "Vendedor:\nO rapaz que traz nossos itens já deve estar fora da vila a essa altura. Volte amanhã.")
                        .WaitForButton()
                ) *
                If(() => timesVisited > 2,
                    Write(0.5f, "Vendedor:\nArgh... Por que tudo tem que ser longe da vila...").WaitForButton()
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