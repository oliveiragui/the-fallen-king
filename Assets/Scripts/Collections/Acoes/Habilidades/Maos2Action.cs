using System.Collections;
using Collections.Avatares;
using UnityEngine;

namespace Collections.Acoes.Habilidades
{
    public class Maos2Action : AbilityAction
    {
        public override void OnStateEntered()
        {
            base.OnStateEntered();
            Avatar.StartCoroutine(AplicaDano());
        }

        IEnumerator AplicaDano()
        {
            yield return new WaitUntil(() =>
                Habilidade.Parametros.TempoCorrido > Habilidade.Modelo.Status.Duracao * 2 / 3);

            var tr = Avatar.transform;

            var hits = Physics.SphereCastAll(tr.position, Habilidade.Modelo.Status.Alcance, tr.forward, 0.01f,
                LayerMask.GetMask("Atingivel"));

            foreach (var hit in hits)
            {
                var outroAvatar = hit.transform.gameObject.GetComponent<AvatarController>();

                if (outroAvatar.Equals(Avatar)) continue;

                if (outroAvatar.entidade.Equipe == Avatar.entidade.Equipe) continue;
                outroAvatar.Comando.Dano(10);
            }
        }
    }
}