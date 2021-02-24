using UnityEngine;
using Utils.Serializables;

namespace Collections.Avatares.Componentes
{
    public class AvatarAudio : MonoBehaviour
    {
        [SerializeField] AudioSom sons;

        public void TocaSom(SlotSom slot)
        {
            sons[slot].Play();
        }
    }
}