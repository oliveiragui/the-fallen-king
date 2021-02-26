using Collection.Abilities.Collections.Habilidades;
using Collection.Weapons;

namespace Collection.Entities
{
    [System.Serializable]
    public class EntityAbilityManager
    {
        public AbilityModel currentAbility;
        public WeaponModel currentWeapon;
        public int currentCombo;

        public bool CanSwitchAbility(AbilityModel ability)
        {
            return
                IsUsingAbility &&
                !CurrentIsEquals(ability) &&
                ability.Info.CanInterrupt(currentAbility);
        }

        public bool CanInterrupt(AbilityModel ability)
        {
            return ability.Info.CanInterrupt(currentAbility);;
        }

        public bool IsUsingAbility => !(currentAbility is null);

        public bool CurrentIsEquals(AbilityModel ability)
        {
            return currentAbility.Info.Id == ability.Info.Id;
        }
    }
}