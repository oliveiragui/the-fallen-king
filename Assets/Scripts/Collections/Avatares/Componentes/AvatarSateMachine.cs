using System.Linq;
using Collections.Acoes;
using Components.StateMachinePattern;

namespace Collections.Avatares.Componentes
{
    public class AvatarSateMachine : SateMachine
    {
        public void Configura(AvatarController avatarBase)
        {
            var habilidades = avatarBase.entidade.Habilidades;
            var parametros = avatarBase.Parametros;

            var movimentoAction = avatarBase.Actions.AddComponent<MovimentoAction>();
            var movimentoMeshAction = avatarBase.Actions.AddComponent<MovimentaNavMeshAction>();
            var idleAction = avatarBase.Actions.AddComponent<IdleAction>();
            var morteAction = avatarBase.Actions.AddComponent<MorteAction>();
            var usaHabilidadeAction = avatarBase.Actions.AddComponent<UsaHabilidadeAction>();

            movimentoMeshAction.Inicializa(avatarBase);
            movimentoAction.Inicializa(avatarBase);
            idleAction.Inicializa(avatarBase);
            morteAction.Inicializa(avatarBase);
            usaHabilidadeAction.Inicializa(avatarBase);

            idleAction.Transitions.Add(new StateTransition(movimentoMeshAction,
                () => parametros.EmMovimento && avatarBase.Parametros.UsaMesh));
            movimentoMeshAction.Transitions.Add(new StateTransition(idleAction, () => !parametros.EmMovimento));

            idleAction.Transitions.Add(new StateTransition(movimentoAction,
                () => parametros.EmMovimento && !avatarBase.Parametros.UsaMesh));
            movimentoAction.Transitions.Add(new StateTransition(idleAction, () => !parametros.EmMovimento));

            morteAction.CanTrasitionToSelf = false;

            foreach (var habilidade in habilidades.Where(habilidade => habilidade != null))
            {
                habilidade.Configura(avatarBase);

                var action = habilidade.Modelo.InstanciaAcao(avatarBase, habilidade);

                usaHabilidadeAction.Transitions.Add(new StateTransition(action,
                    () => habilidade.Modelo.Equals(habilidades[parametros.HabilidadeSolicitada].Modelo)));

                action.Transitions.Add(new StateTransition(idleAction, () => !habilidade.Parametros.EmUso));
            }

            var transicaoHabilidade = new StateTransition(usaHabilidadeAction, () =>
            {
                var habilidadeAvatar =
                    habilidades[parametros.HabilidadeSolicitada];
                return
                    parametros.gatilho.Dispara() &&
                    habilidadeAvatar.Parametros.PodeSerUsada &&
                    (avatarBase.HabilidadeAtual == null ||
                     habilidadeAvatar.Modelo.PodeInterromper(avatarBase.HabilidadeAtual.Modelo));
            });

            idleAction.Transitions.Add(transicaoHabilidade);
            movimentoAction.Transitions.Add(transicaoHabilidade);
            usaHabilidadeAction.Transitions.Add(transicaoHabilidade);

            SetState(idleAction);
            Reset();
            SetAnyStateTransitions(new StateTransition(morteAction, () => !parametros.Vivo));
        }
    }
}