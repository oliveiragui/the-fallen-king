using UnityEngine;

namespace Entities.Animation
{
    public class EntityCombatAnimation : MonoBehaviour
    {
        [SerializeField] Animator animator;

        public void EquipWeapon()
        {
            animator.SetLayerWeight(1, 1f);
        }

        public void UnequipWeapon()
        {
            animator.SetLayerWeight(1, 0f);
        }
    }
}