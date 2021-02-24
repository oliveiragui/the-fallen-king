using Collections.Avatares.Componentes;
using UnityEditor.Animations;
using UnityEngine;

namespace Collections.Avatares
{
    [CreateAssetMenu(fileName = "Avatar", menuName = "GameContent/Avatar", order = 1)]
    public class AvatarModel : ScriptableObject
    {
        [SerializeField] AvatarMesh _avatarMesh;
        [field: SerializeField] public AnimatorController AnimatorController { get; private set; }

        public GameObject InstantiateAvatarMesh(Transform transform)
        {
            return Instantiate(_avatarMesh.gameObject, transform);
        }
    }
}