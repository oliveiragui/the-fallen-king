using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class DialogoTreinoBotao : FluentScript
    {
        [SerializeField] Entity character;
        [SerializeField] UnityEvent onStart;
        [SerializeField] UnityEvent onFinish;
        [SerializeField] UnityEvent onFinishTwoTimes;
        int timesVisited;

        public override FluentNode Create()
        {
            return
                Show() *
                Do(() => timesVisited++) *
                If(() => timesVisited == 1,
                    Write(0.5f, "Dragg:\nVolte a falar comigo quando estiver pronto pro treino!").WaitForButton()
                ) *
                If(() => timesVisited >= 2,
                    Write(0.5f, "Dragg:\nPara atacar aperte <sprite=\"KeyBoardAndMouse\" name=\"Mouse_Left_Key_Dark\"> ")
                        .WaitForButton() *
                    Write(0.5f, "Dragg:\nPara trocar de arma aperte <sprite=\"KeyBoardAndMouse\" name=\"Tab_Key_Dark\"> ")
                        .WaitForButton()
                ) *
                Hide();
        }

        public override void OnFinish()
        {
            if (timesVisited >= 2)
                onFinish.Invoke();
            else
                onFinishTwoTimes.Invoke();
        }

        public override void OnStart()
        {
            character.Stop();
            onStart.Invoke();
        }
    }
}