using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoTerminaCombatePreBoss : FluentScript
{
    public GameObject camera;
    public GameObject timeline;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    [SerializeField] UnityEvent dialogueTrigger;
    private int enemyDeathCount = 0;
    public override FluentNode Create()
    {
        return
             Yell(3.5f, "General:\nHAR HAR HAR HAR HAR...") *
             Do(() => AtivaGameObject(camera)) *
             Show() *
             Write(0.5f, "General:\nMoleque!! Parece que os meus soldados não são capazes de te derrotar!").WaitForButton() *
             Write(0.5f, "General:\nHAR HAR HAR!! Esses vermes só servem pra atacar gente indefesa mesmo...").WaitForButton() *
             Write(0.5f, "General:\nNão posso te deixar vivo depois de matar todos da sua vila...").WaitForButton() *
             Write(0.5f, "General:\nEles devem estar sentindo sua falta... HAR HAR HAR HAR").WaitForButton() *
             Hide() *
             Do(() => AtivaGameObject(timeline));
    }


    private void AtivaGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);

        }
    }

    public void EnemyDied()
    {
        enemyDeathCount++;

        if (enemyDeathCount == 5)
            DialogueTrigger();
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
    private void DialogueTrigger()
    {
        dialogueTrigger.Invoke();
    }

}