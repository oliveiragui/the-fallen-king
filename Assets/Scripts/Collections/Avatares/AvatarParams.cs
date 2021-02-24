using UnityEngine;
using Utils;

namespace Collections.Avatares
{
    public class AvatarParams
    {
        public readonly Gatilho gatilho = new Gatilho();
        readonly AvatarController _avatar;
        public bool atacando = false;
        public bool Conjurando = false;
        public bool EmMovimento = false;
        public int HabilidadeSolicitada;
        public Vector3 PontoMovimento;
        public Vector3 PontoOlhar;
        public bool Vivo = true;

        public AvatarParams(AvatarController avatar)
        {
            _avatar = avatar;
        }

        public float DirecaoMovimento { get; set; }
        public float DirecaoOlhar { get; set; }

        public bool UsaMesh { get; set; }

        public bool Atingivel
        {
            get => _avatar.Colisores.gameObject.CompareTag("Atingivel");
            set => _avatar.Colisores.gameObject.tag = value ? "Atingivel" : "Atingivel";
        }
    }
}