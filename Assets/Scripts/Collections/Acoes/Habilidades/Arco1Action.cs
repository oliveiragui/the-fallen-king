using System.Collections;
using Collections.Avatares.Componentes;
using UnityEngine;

namespace Collections.Acoes.Habilidades
{
    public class Arco1Action : AbilityAction
    {
        Coroutine _rotina;
        bool vaiAtirar;

        public override void OnStateEntered()
        {
            base.OnStateEntered();
            _rotina = Avatar.StartCoroutine(Atualizacao());
        }

        IEnumerator Atualizacao()
        {
            float evento1 = Habilidade.Modelo.Status.Duracao * 1 / 3;
            float evento2 = Habilidade.Modelo.Status.Duracao * 3 / 4;

            vaiAtirar = true;
            yield return ActionEvents.AguardaAte(Habilidade, evento1);
            yield return ActionEvents.AguardaDesparo(Avatar, Habilidade);

            Avatar.Audio.TocaSom(SlotSom.ArcoDesparando);
            Avatar.Audio.TocaSom(SlotSom.FlechaDesparada);
            vaiAtirar = false;

            yield return ActionEvents.AguardaAte(Habilidade, evento2);
            ActionEvents.DisparaFlecha(Avatar, Habilidade, evento2);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (vaiAtirar) Avatar.transform.rotation = Quaternion.Euler(0, Avatar.Parametros.DirecaoOlhar, 0);
        }

        public override void OnStateExited()
        {
            Avatar.Animacao.ConjuraHabilidade(false);
            base.OnStateExited();
            if (!(_rotina is null)) Avatar.StopCoroutine(_rotina);
        }
    }
}