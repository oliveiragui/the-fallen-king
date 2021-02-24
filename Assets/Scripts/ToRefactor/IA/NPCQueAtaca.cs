using Collections.Avatares;
using Collections.Avatares.Componentes;
using UnityEngine;
using Utils.Extension;

namespace ToRefactor.IA
{
    public class NPCQueAtaca : MonoBehaviour
    {
        public bool PodeAtacar;

        public NPCcomIA npcComIA;
        [SerializeField] AvatarController avatar;

        AvatarComando comandoAvatar;
        int distanciaDeAtaque;

        void Start()
        {
            comandoAvatar = new AvatarComando(avatar);
            distanciaDeAtaque = 8;
        }

        void FixedUpdate()
        {
            if (PodeAtacar && npcComIA.target != null)
                ProcessaInput();
        }

        void ProcessaInput()
        {
            var targetDistance = npcComIA.target.position - avatar.transform.position;
            float dirMov = new Vector2(targetDistance.x, targetDistance.z).ToDegree() + 90;

            comandoAvatar.OlhaParaDirecao(dirMov, dirMov);

            if (targetDistance.magnitude < distanciaDeAtaque)
            {
                if (!avatar.Parametros.atacando)
                {
                    comandoAvatar.UsaHabilidade(avatar.entidade.Habilidades[0].Modelo.AnimationID);
                    comandoAvatar.ConjuraHabilidade(false);
                }
            }
            else
            {
                if (!avatar.Parametros.atacando) comandoAvatar.MovimentaAtePonto(npcComIA.target.position);
            }
        }
    }
}