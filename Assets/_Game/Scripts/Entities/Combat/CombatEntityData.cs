using Abilities;
using Entities.Common.Animation;
using UnityEngine;

namespace Entities.Combat
{
    public class CombatEntityData : MonoBehaviour
    {
        public EntityCombatAnimation animations;
        public bool conjuring;
        public bool inCombat;
        public Ability[] abilities;
        public Ability CurrentAbility;
        public AbilityCombo currentComboData;
        public bool UsingAbility { get; set; }
        public bool UsingCombo { get; set; }
    }
}