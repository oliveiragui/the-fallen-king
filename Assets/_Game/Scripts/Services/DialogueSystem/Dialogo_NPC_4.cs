using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class Dialogo_NPC_4 : FluentScript
    {
        [SerializeField] Entity character;
        [SerializeField] UnityEvent onStart;
        [SerializeField] UnityEvent onFinish;
        int timesVisited = 0;

        public override FluentNode Create() =>
            Show() *
            Write(0.5f, "Vendedor:\nRapaz, não vou poder te atender, tô fechando a loja agora.").WaitForButton() *
            Write(0.5f,
                    "Vendedor:\nTenho que ir na enfermaria, aquele médico disse que consegue dar um jeito nas minhas costas")
                .WaitForButton() *
            Write(0.5f, "Vendedor:\nHehehe... finalmente vou me livrar dessa maldita dor...").WaitForButton() *
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