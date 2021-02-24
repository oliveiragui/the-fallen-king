using System.Collections;
using System.Collections.Generic;
using Collections.Avatares;
using Collections.Avatares.Componentes;
using UnityEngine;

namespace ToRefactor.IA
{
    public class NPCObservaPontos : MonoBehaviour
    {
        public List<Transform> pontosDeExploracao;

        [SerializeField] AvatarController avatar;

        public bool PodeAndar;

        AvatarComando comandoAvatar;

        void Start()
        {
            comandoAvatar = new AvatarComando(avatar);
            StartCoroutine(Rotina());
        }

        IEnumerator Rotina()
        {
            var pontos = pontosDeExploracao.GetEnumerator();
            pontos.MoveNext();

            while (true)
            {
                if (PodeAndar)
                {
                    var ponto = pontos.Current;

                    yield return new WaitWhile(() =>
                    {
                        var position = ponto.position;
                        comandoAvatar.MovimentaAtePonto(position);
                        return (avatar.transform.position - position).magnitude > 2;
                    });

                    if (!pontos.MoveNext())
                    {
                        pontos = pontosDeExploracao.GetEnumerator();
                        pontos.MoveNext();
                    }
                }

                yield return null;
            }
        }
    }
}