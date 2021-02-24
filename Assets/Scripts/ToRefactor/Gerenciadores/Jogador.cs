using System.Collections;
using Collections.Entidades;
using ToRefactor.Controladores;
using ToRefactor.Controladores.Enums;
using ToRefactor.UI;
using UnityEngine;

namespace ToRefactor.Gerenciadores
{
    public class Jogador : MonoBehaviour
    {
        public Entidade entidade;
        [field: SerializeField] public Controlador Controlador { get; private set; }

        IEnumerator Start()
        {
            GerenciadorDeJogo.Instance.Jogadores[JogadorID.P1] = this;
            Controlador.Configura(entidade);

            entidade.Bainha.AoMudarArma.AddListener(equip => AtualizaCartoesHabilidade());
            yield return new WaitUntil(() => Interface.MenuPersonagem.AbaHabilidade != null);
            AtualizaCartoesHabilidade();
        }

        void Update()
        {
            if (entidade.status.VidaAtual.Valor <= 0) GerenciadorDeJogo.ReiniciaFase();
        }

        void AtualizaCartoesHabilidade()
        {
            Interface.MenuPersonagem.AbaHabilidade
                .AtualizaCartoes(Controlador.Controle.ativo, entidade.Habilidades);
            Interface.MenuPersonagem.AbaInventario.Atualiza(entidade);
        }
    }
}