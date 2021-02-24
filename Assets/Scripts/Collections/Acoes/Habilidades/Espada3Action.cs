using System.Collections;
using Collections.Avatares.Componentes;
using UnityEngine;

namespace Collections.Acoes.Habilidades
{
    public class Espada3Action : AbilityAction
    {
        public override void OnStateEntered()
        {
            base.OnStateEntered();
            Avatar.StartCoroutine(Atualizacao());
        }

        IEnumerator Atualizacao()
        {
            float evento1 = Habilidade.Modelo.Status.Duracao * 1 / 2;
            Avatar.Audio.TocaSom(SlotSom.ArcoDesparando);
            yield return new WaitUntil(() => Habilidade.Parametros.TempoCorrido > evento1);
            yield return EscudoLevantado();
        }

        IEnumerator EscudoLevantado()
        {
            Habilidade.Cronometro.Espera();
            yield return new WaitUntil(() => !Avatar.Parametros.Conjurando);
            Avatar.Animacao.ConjuraHabilidade(Avatar.Parametros.Conjurando);
            Habilidade.Cronometro.Continua();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            Avatar.transform.rotation = Quaternion.Euler(0, Avatar.Parametros.DirecaoOlhar, 0);
        }
    }
}