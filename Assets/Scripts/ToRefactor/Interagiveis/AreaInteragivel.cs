using Collections.Avatares;
using Collections.Controles.Utils;
using ToRefactor.Controladores;
using ToRefactor.Controladores.Enums;
using ToRefactor.Gerenciadores;
using UnityEngine;

namespace ToRefactor.Interagiveis
{
    public class AreaInteragivel : MonoBehaviour
    {
        public bool interagiu;
        public bool jogadorDentro;
        public AvatarController jogador;
        Controlador ctrl;

        void OnTriggerEnter(Collider other)
        {
            if (!other.tag.Equals("Interagivel")) return;
            var avatar = other.GetComponent<Interagivel>().atingivel;

            if (!avatar.entidade.Equipe.aliado || jogador.entidade.Equipe.npc) return;

            jogador = avatar;
            jogadorDentro = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.tag.Equals("Interagivel")) return;
            jogador = other.GetComponent<Interagivel>().atingivel;

            if (jogador.entidade.Equipe.npc || !jogador.entidade.Equipe.aliado) return;

            jogadorDentro = false;
            interagiu = false;
        }

        void OnTriggerStay(Collider other)
        {
            if (jogador == null || !other.tag.Equals("Interagivel")) return;

            if (ctrl != null) interagiu = ctrl.Controle.ativo.GetButtonDown(Botao.Confirma);
            else ctrl = GerenciadorDeJogo.Instance.Jogadores[JogadorID.P1].Controlador;
        }
    }
}