using System;
using System.Linq;
using Collections.Controles.Utils;
using ToRefactor.Controladores.Enums;
using UnityEngine;

namespace Collections.Controles
{
    public class Controle
    {
        public readonly ControleModelo perfil;
        public readonly string prefixo;

        public Controle(ControleModelo perfil, JogadorID jogadorID)
        {
            this.perfil = perfil;
            prefixo = jogadorID.ToString() + (char) perfil.Tipo;
        }

        public bool AlgumEixoAtivado =>
            Enum.GetValues(typeof(Eixo)).Cast<Eixo>().Any(eixo => Input.GetAxis(Eixo(eixo)) > 0);

        public bool AlgumBotaoPressionado => Enum.GetValues(typeof(Botao)).Cast<Botao>()
            .Any(botao => Input.GetButton(Botao(botao)));

        public bool ControleUsado => AlgumEixoAtivado || AlgumBotaoPressionado;

        public string Eixo(Eixo eixo) => prefixo + eixo;

        public string Botao(Botao botao) => prefixo + botao;

        public float GetAxis(Eixo eixo) => Input.GetAxis(Eixo(eixo));

        public float GetAxisRaw(Eixo eixo) => Input.GetAxisRaw(Eixo(eixo));

        public bool GetButton(Botao botao) => Input.GetButton(Botao(botao));

        public bool GetButtonUp(Botao botao) => Input.GetButtonUp(Botao(botao));

        public bool GetButtonDown(Botao botao) => Input.GetButtonDown(Botao(botao));
    }
}