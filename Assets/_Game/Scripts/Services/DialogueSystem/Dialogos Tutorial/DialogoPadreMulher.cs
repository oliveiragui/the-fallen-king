using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DialogoPadreMulher : FluentScript
{
    public GameObject timeline;
    public GameObject camera;

    public override FluentNode Create()
    {
        return
            Yell(3.0f, "Médico:\n Você já pode entrar agora.") *
            Do(() => camera.SetActive(true)) *
            Show() *
            Write(0.5f, "Mulher:\n Doutor, só queria te agradecer por tudo que você fez por mim e pelos outros! Realmente é um milagre que o senhor esteja na nossa vila!").WaitForButton() *
            Write(0.5f, "Médico:\n Não precisa me agradecer...").WaitForButton() *
            Write(0.5f, "Médico:\n Lembre-se de descansar por 3 dias e você já estará melhor.").WaitForButton() *
            Write(0.5f, "Mulher:\n Amanhã eu volto para ver como está minha irmãzinha").WaitForButton() *
            Write(0.5f, "Mulher:\n Até mais").WaitForButton() *
            Hide() *
            Do(() => IniciaCutscene());
    }

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }

    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            timeline.SetActive(true);

        }
    }
}
