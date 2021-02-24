using Collections.Entidades;
using ToRefactor.UI.HUD.EntidadeHUD;
using UnityEngine;

namespace ToRefactor.IA
{
    public class NPCcomIA : MonoBehaviour
    {
        public Transform target;
        public AchaInimigoEmArea achaInimigoEmArea;
        public NPCQueAtaca npcQueAtaca;
        public AvatarHUD avatarHUD;
        public Entidade entidade;
    }
}