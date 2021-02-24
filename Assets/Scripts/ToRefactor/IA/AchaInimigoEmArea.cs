using Collections.Avatares;
using ToRefactor.Interagiveis;
using UnityEngine;

namespace ToRefactor.IA
{
    public class AchaInimigoEmArea : MonoBehaviour
    {
        [SerializeField] AvatarController avatar;
        public bool poderProcurarInimigo;
        public NPCcomIA npcComIA;
        AreaInteragivel areaDeAnalise;

        void FixedUpdate()
        {
            if (poderProcurarInimigo && npcComIA.target == null) EncontraInimigo();
        }

        void EncontraInimigo()
        {
            if (areaDeAnalise.jogadorDentro) npcComIA.target = areaDeAnalise.jogador.transform;
        }
    }
}