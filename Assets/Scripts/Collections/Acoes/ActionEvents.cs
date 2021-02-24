using System.Collections;
using Collections.Armas;
using Collections.Avatares;
using Collections.Habilidades;
using ToRefactor;
using UnityEngine;

namespace Collections.Acoes
{
    public static class ActionEvents
    {
        public static IEnumerator AguardaAte(HabilidadeController habilidade, float tempo)
        {
            return new WaitUntil(() => habilidade.Parametros.TempoCorrido > tempo);
        }

        public static IEnumerator AguardaDesparo(AvatarController avatar, HabilidadeController habilidade)
        {
            habilidade.Cronometro.Espera();
            yield return new WaitUntil(() => !avatar.Parametros.Conjurando);
            avatar.Animacao.ConjuraHabilidade(avatar.Parametros.Conjurando);
            habilidade.Cronometro.Continua();
        }

        public static void DisparaFlecha(
            AvatarController avatar, HabilidadeController habilidade,
            float delay = 0
        )
        {
            var forward = avatar.transform.forward;

            var flecha = Object.Instantiate(Municoes.Instance.FlechaComHit,
                avatar.Mesh.PontoDeTiro.position,
                avatar.transform.rotation);

            var hitboxflecha = flecha.GetComponent<HitBoxFlecha>();
            hitboxflecha.aliado = avatar.entidade.Equipe.aliado;
            var rb = flecha.GetComponent<Rigidbody>();
            rb.velocity = forward * 17;
        }
    }
}