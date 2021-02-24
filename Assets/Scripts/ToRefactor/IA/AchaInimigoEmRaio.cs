using Collections.Avatares;
using UnityEngine;

namespace ToRefactor.IA
{
    public class AchaInimigoEmRaio : MonoBehaviour
    {
        [SerializeField] AvatarController avatar;
        public bool poderProcurarInimigo;
        public NPCcomIA npcComIA;

        void FixedUpdate()
        {
            if (poderProcurarInimigo && npcComIA.target == null)
                EncontraInimigo(avatar.transform.position, 8);
            else
            {
                InimigoDistante();
            }
        }

        void EncontraInimigo(Vector3 center, float radius)
        {
            var hitColliders = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Atingivel"));

            foreach (var collider in hitColliders)
                if (!collider.transform.Equals(avatar.transform))
                {
                    npcComIA.target = collider.transform;
                    return;
                }
        }

        void InimigoDistante()
        {
            if (npcComIA.target && (avatar.transform.position - npcComIA.target.position).magnitude > 10)
                npcComIA.target = null;
        }
    }
}