using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoMedicoPosBoss : FluentScript
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject timeline;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    [SerializeField] UnityEvent dialogueTrigger;
    
    public override FluentNode Create()
    {
        return
            Show() *
            Write(2.5f, "Médico:\nEi... Tá acordado?").WaitForButton() *
            Write(2.5f, "Médico:\nVocê parece estar acordado.").WaitForButton() *
            Write(2.5f, "Médico:\nO que você fez foi bem impressionante, digo, derrotar o General Labahn." +
                        " Pra alguém que tinha inalado tanta fumaça do incêndio, você resistiu muito bem.").WaitForButton() *
            Write(2.5f, "Você é o médico... O quê? Você conhecia aquele grandão?").WaitForButton() *
            Write(2.5f, "Médico:\nPermita-me introduzir meu nome primeiro. Eu sou Pirin, do Reino do Norte, o mesmo lugar do exército que você acabou de enfrentar.").WaitForButton() *
            Write(2.5f, "Pirin:\nAntes de você começar a me questionar, e aproveitando que você está em repouso, vou te contar o que está acontecendo...").WaitForButton() *
            Write(2.5f, "Pirin:\nAqueles soldados que você enfrentou são do exército real do Reino do Norte, e o general Labahn é um dos 8 generais do exército.").WaitForButton() *
            Write(2.5f, "Certo. Então por que eles atacaram a vila? Nós nem somos afiliados a Reino nenhum!").WaitForButton() *
            Write(2.5f, "Pirin:\nEu vou chegar lá. O Reino do Norte está passando por uma crise nesse momento. O Rei está adoecido e, pra falar a verdade, ele mal consegue se comunicar." +
                                                 " Logo seu sucessor deve ser anunciado.").WaitForButton() *
            Write(2.5f, "Pirin:\nApesar do Rei não ser uma figura mal vista pelo povo, há uma oposição a família real, um grupo chamado Legisladores. " +
                                                 "Eles pretendem acabar com a monarquia e instituir um representante eleito pelo povo para governar a terra").WaitForButton() *
            Write(2.5f, "Pirin:\nOs Legisladores descobriram que o Rei possui um filho mais velho, mas ele é bastardo. A história vazada por eles é que, há 20 anos atrás, o Rei teve seu primeiro filho em um romance com uma das enfermeiras do castelo.").WaitForButton() *
            Write(2.5f, "Pirin:\nO óbvio é que esse filho não poderia vir ao público, para não prejudicar a imagem do Rei e a relação com os outros reinos.").WaitForButton() *
            Write(2.5f, "Pirin:\nO Rei amava de verdade a mulher e a criança, e em suas viagens sempre dava um jeito de estar com eles de maneira escondida, mas eles não podiam viver no castelo para ninguém descobrir.").WaitForButton() *
            Write(2.5f, "Pirin:\nO Rei também teve outros filhos de sangue real, mas isso pouco importa, e os Legisladores sabem disso, por que a sucessão do trono é direito do filho mais velho.").WaitForButton() *
            Write(2.5f, "Pirin:\nO General Labahn fazia parte do grupo de 6 generais sob controle direto da nobreza ligada a familia do segundo filho do Rei.").WaitForButton() *
            Write(2.5f, "Pirin:\nRecentemente, os Legisladores anunciaram que o primogênito do Rei fazia parte do grupo deles, e que quando assumisse o trono, iria renunciar a monarquia.").WaitForButton() *
            Write(2.5f, "Pirin:\nOs 6 generais descobriram onde era o esconderijo dos Legisladores, recolheram seus nomes, profissões... Muitos estão em fuga ou escondidos no reino.").WaitForButton() *
            Do(() => AtivaGameObject(camera1)) *
            Write(3.0f, "Pirin:\nO exército tem destruído muitas cidades e vilas buscando os membros que fugiram, a monarquia está disposta ao que for para destruir o sonho dos Legisladores...").WaitForButton() *
            Write(2.5f, "Então o que aconteceu aqui...").WaitForButton() *
            Write(2.5f, "Pirin:\nExatamente. Essa vila também foi pega no meio desse caos todo...").WaitForButton() *
            Write(2.5f, "Eu diria que você é um dos Legisladores...").WaitForButton() *
            Do(() => AtivaGameObject(camera2)) *
            Write(2.5f, "Pirin:\nEles mataram meus amigos... a nobreza tem negado tratamento médico e comida para o povo enquanto a perseguição perdura...").WaitForButton() *
            Write(2.5f, "Pirin:\nVocê não está errado, eu sou um membro dos Legisladores. Bem, é um pouco mais complicado que isso...").WaitForButton() *
            Write(2.5f, "Pirin:\nNormalmente, os generais não participam diretamente das perseguições, a menos que um membro importante dos Legisladores esteja envolvido.").WaitForButton() *
            Write(2.5f, "Pirin:\nEu sempre tive um dom para medicina, minha mãe era uma enfermeira, ela me ensinou muito bem sobre como tratar cada paciente, mas não posso dizer o mesmo sobre combate. " +
                                                 "Se o exército tivesse me achado antes de você derrota-los, é provável que eu seria capturado").WaitForButton() *
            Do(() => AtivaGameObject(camera3)) *
            Write(2.5f, "Você disse que sua mãe era enfermeira. Quer dizer que você...").WaitForButton() *
            Write(1.5f, "Pirin:\nOh...").WaitForButton() *
            Do(() => AtivaGameObject(timeline)) *
            Hide();
    }


    private void AtivaGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
        onStart.Invoke();
    }

    private void DialogueTrigger()
    {
        dialogueTrigger.Invoke();
    }
}