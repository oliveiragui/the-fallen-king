using System;
using Abilities.Collections.Habilidades;

namespace Entities
{
    [Serializable]
    public class EntityAbilityManager
    {
        public AbilityData currentAbility;

        public bool IsUsingAbility => !(currentAbility is null);

        public bool CanSwitchAbility(AbilityData ability)
        {
            return
                IsUsingAbility &&
                !CurrentIsEquals(ability) &&
                ability.CanInterrupt(currentAbility);
        }

        public bool CanInterrupt(AbilityData ability)
        {
            return ability.CanInterrupt(currentAbility);
            ;
        }

        public bool CurrentIsEquals(AbilityData ability)
        {
            return currentAbility.Id == ability.Id;
        }
    }
}