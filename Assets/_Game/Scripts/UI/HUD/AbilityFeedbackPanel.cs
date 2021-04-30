using _Game.Scripts.GameContent.Abilities;
using UnityEngine;

namespace _Game.Scripts.UI.HUD
{
    public class AbilityFeedbackPanel : MonoBehaviour
    {
        [SerializeField] AbilityIcon[] abilityIcons;

        public string[] buttonsName = {"X", "Y", "B", "A"};

        public void OnWeaponChange(Ability[] abilities)
        {
            for (var i = 0; i < abilities.Length && i < buttonsName.Length && i < abilityIcons.Length; i++)
                abilityIcons[i].BindAbility(abilities[i], buttonsName[i]);
        }

        void UpdateAndAddListener() { }
    }
}