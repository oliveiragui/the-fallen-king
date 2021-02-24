using System.Linq;
using Collections.Armas;
using UnityEditor.Animations;
using UnityEngine;
using Utils.Serializables;

namespace Collections.Avatares.Componentes
{
    public class AvatarMesh : MonoBehaviour
    {
        [SerializeField] public AnimatorController animationController;
        [SerializeField] public Avatar avatar;
        [SerializeField] public Transform PontoDeTiro;
        [SerializeField] public SlotArmasPrefabs slots;

        public void TrocaArma(ArmaController arma)
        {
            foreach (var child in slots.Values.SelectMany(slot => slot.transform.Cast<Transform>()))
                Destroy(child.gameObject);

            foreach (var prefab in arma.Modelo.Prefabs)
            {
                if (!slots.ContainsKey(prefab.Slot)) continue;
                Instantiate(prefab.GameObject, slots[prefab.Slot].transform);
            }
        }
    }
}