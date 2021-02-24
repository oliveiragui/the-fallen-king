using System.Collections;
using Collections.Armas;
using Collections.Avatares.Componentes;
using ToRefactor;
using UnityEngine;

namespace Collections.Acoes.Habilidades
{
    public class Arco2Action : AbilityAction
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
            yield return new WaitUntil(() => Habilidade.Parametros.TempoCorrido > evento1);

            yield return AguardaDesparado();

            Avatar.Audio.TocaSom(SlotSom.ArcoDesparando);
            Avatar.Audio.TocaSom(SlotSom.FlechaDesparada);

            vaiAtirar = false;
            yield return new WaitUntil(() => Habilidade.Parametros.TempoCorrido > evento2);
            DisparaFlecha();
        }

        IEnumerator AguardaDesparado()
        {
            Habilidade.Cronometro.Espera();
            yield return new WaitUntil(() => !Avatar.Parametros.Conjurando);
            Avatar.Animacao.ConjuraHabilidade(Avatar.Parametros.Conjurando);
            Habilidade.Cronometro.Continua();
        }

        void DisparaFlecha()
        {
            var forward = Avatar.transform.forward;

            var flecha = Instantiate(Municoes.Instance.FlechaComHit,
                Avatar.Mesh.PontoDeTiro.position,
                Avatar.transform.rotation);

            var hitboxflecha = flecha.GetComponent<HitBoxFlecha>();

            hitboxflecha.aliado = Avatar.entidade.Equipe.aliado;

            var rb = flecha.GetComponent<Rigidbody>();
            rb.velocity = forward * 17;
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