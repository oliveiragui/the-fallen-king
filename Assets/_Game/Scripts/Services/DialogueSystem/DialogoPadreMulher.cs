using Fluent;
using UnityEngine;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class DialogoPadreMulher : FluentScript
    {
        public GameObject timeline;
        public GameObject camera;

        public override FluentNode Create()
        {
            return
                Show() *
                Write(2.0f, "Médico:\n Você já pode entrar agora.").WaitForButton() *
                Do(() => camera.SetActive(true)) *
                Write(0.5f,
                        "Mulher:\n Doutor, só queria te agradecer por tudo que você fez por mim e pelos outros! Realmente é um milagre que o senhor esteja na nossa vila!")
                    .WaitForButton() *
                Write(0.5f, "Médico:\n Não precisa me agradecer...").WaitForButton() *
                Write(0.5f, "Médico:\n Lembre-se de descansar por 3 dias e você já estará melhor.").WaitForButton() *
                Write(0.5f, "Mulher:\n Amanhã eu volto para ver como está minha irmãzinha").WaitForButton() *
                Write(0.5f, "Mulher:\n Até mais").WaitForButton() *
                Hide() *
                Do(() => IniciaCutscene());
        }

        public override void OnFinish() { }

        public override void OnStart() { }

        void IniciaCutscene()
        {
            if (timeline != null) timeline.SetActive(true);
        }
    }
}