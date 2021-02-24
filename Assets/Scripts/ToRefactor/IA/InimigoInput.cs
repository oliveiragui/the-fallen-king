using System.Collections;
using System.Collections.Generic;
using Collections.Avatares;
using Collections.Avatares.Componentes;
using UnityEngine;
using Utils.Extension;

namespace ToRefactor.IA
{
    public class InimigoInput : MonoBehaviour
    {
        public Transform target;
        public List<Transform> pontosDeExploracao;

        [SerializeField] AvatarController avatar;

        AvatarComando comandoAvatar;

        void Start()
        {
            comandoAvatar = new AvatarComando(avatar);
            StartCoroutine(Rotina());
        }

        void FixedUpdate()
        {
            if (target == null)
                EncontraInimigo(avatar.transform.position, 8);
            else
                ProcessaInput();

            InimigoDistante();
        }

        void ProcessaInput()
        {
            var targetDistance = target.position - avatar.transform.position;
            float dirMov = new Vector2(targetDistance.x, targetDistance.z).ToDegree() + 90;

            comandoAvatar.OlhaParaDirecao(dirMov, dirMov);

            if (targetDistance.magnitude < 5)
            {
                comandoAvatar.UsaHabilidade(0);
                comandoAvatar.ConjuraHabilidade(false);
            }
            else
            {
                comandoAvatar.MovimentaAtePonto(target.position);
            }
        }

        void EncontraInimigo(Vector3 center, float radius)
        {
            var hitColliders = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Atingivel"));

            foreach (var collider in hitColliders)
                if (!collider.transform.Equals(avatar.transform))
                {
                    target = collider.transform;
                    return;
                }
        }

        void InimigoDistante()
        {
            if (target)
                if ((avatar.transform.position - target.position).magnitude > 10)
                {
                    target = null;
                    StartCoroutine(Rotina());
                }
        }

        IEnumerator Rotina()
        {
            var pontos = pontosDeExploracao.GetEnumerator();
            pontos.MoveNext();

            while (target == null)
            {
                var ponto = pontos.Current;

                yield return new WaitWhile(() =>
                {
                    comandoAvatar.MovimentaAtePonto(ponto.position);
                    return target == null && (avatar.transform.position - ponto.position).magnitude > 2;
                });

                if (!pontos.MoveNext())
                {
                    pontos = pontosDeExploracao.GetEnumerator();
                    pontos.MoveNext();
                }
            }
        }
    }
}