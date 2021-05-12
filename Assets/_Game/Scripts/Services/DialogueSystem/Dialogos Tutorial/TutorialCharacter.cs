using System;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.IA
{
    public class TutorialCharacter : FluentScript
    {
        [SerializeField] Character character;
        [SerializeField] Entity characterMove;
        [SerializeField] UnityEvent onStart;
        [SerializeField] UnityEvent onFinish;

        public void Reset()
        {
            character.CharacterStatus.Life.Current += 1000000;
            //FluentManager.Instance.AddScript(GetComponent<FluentScript>()); *
            FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
        }

        public GameObject timeline;

        public override FluentNode Create()
        {
            return
                Show() *
                Write(1.0f, "Dragg:\n Você aprendeu muito, jovem.").WaitForButton() *
                Write(0.5f, "Dragg:\n Terei que ir agora, mas na próxima juro que acabo contigo...").WaitForButton() *
                Write(0.5f, "Dragg:\n Acho melhor você também voltar pra casa e descansar, acabamos treinando até tarde...").WaitForButton() *
                Write(0.5f, "Dragg:\n Aproveite para conversar com as pessoas da vila antes de ir dormir.").WaitForButton() *
                Write(0.5f, "Dragg:\n Até mais.").WaitForButton() *
                Hide();
            
        }

        public override void OnFinish()
        {
            onFinish.Invoke();
        }

        public override void OnStart()
        {
            characterMove.Stop();
            onStart.Invoke();
        }

        private void IniciaCutscene()
        {
            if (timeline != null)
            {
                timeline.SetActive(true);
            }
        }
    }
}