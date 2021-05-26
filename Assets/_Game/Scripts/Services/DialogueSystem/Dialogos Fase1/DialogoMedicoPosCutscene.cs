using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoMedicoPosCutscene : FluentScript
{
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Pirin:\nBom, assim que estiver pronto, me espere na saída da vila. Vou terminar de tratar os pacientes e recolher meus pertences e te encontro lá").WaitForButton() *
            Hide();
    }
    
    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
        character.StopWalking();
        onStart.Invoke();
    }

}