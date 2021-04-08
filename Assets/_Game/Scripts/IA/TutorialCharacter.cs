using System;
using _Game.Scripts.GameContent.Characters;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.IA
{
    public class TutorialCharacter : FluentScript
    {
        [SerializeField] Character character;
        [SerializeField] UnityEvent onStart;
        [SerializeField] UnityEvent onFinish;

        public void Reset()
        {
            character.Status.Life.ApplyDamage(100000000000);
            //FluentManager.Instance.AddScript(GetComponent<FluentScript>());
            FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
        }

        public GameObject timeline;

        public override FluentNode Create()
        {
            return
                Show() *
                Write(1.0f, "Dragg:\n Você aprendeu muito, jovem.").WaitForButton() *
                Write(0.5f, "Dragg:\n Terei que ir agora, mas na próxima juro que acabo contigo...").WaitForButton() *
                Write(0.5f, "Dragg:\n Aproveite para conversar com as pessoas da vila.")
                    .WaitForButton() *
                Write(0.5f, "Dragg:\n Até mais.") *
                Options
                (
                    Option("") *
                    Hide() *
                    Do(() => IniciaCutscene()) *
                    End()
                );
        }

        public override void OnFinish()
        {
            onFinish.Invoke();
            //FluentManager.Instance.RemoveScript(GetComponent<FluentScript>());
        }

        public override void OnStart()
        {
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