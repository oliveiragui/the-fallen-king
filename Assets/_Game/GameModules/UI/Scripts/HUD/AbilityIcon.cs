using System.Collections;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Weapons.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class AbilityIcon : MonoBehaviour, IWeaponChangeListener
    {
        [SerializeField] int abilityIndex;
        [SerializeField] string buttonName;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI buttonIcon;

        [SerializeField] Image UsageIndicator;

        [SerializeField] TextMeshProUGUI cooldownTimer;
        [SerializeField] Image cooldownIndicator;

        Coroutine cooldownCount;

        Ability currentAbility;

        public void OnWeaponChange(Weapon weapon)
        {
            buttonIcon.text = $"<sprite=\"XboxOne\" name=\"XboxOne_{buttonName}\">";
            var ability = weapon.Abilities[abilityIndex];
            icon.sprite = ability.Data.MetaData.Icon;
            if (currentAbility) RemoveCurrentAbility();
            AddAbility(ability);
        }

        void AddAbility(Ability ability)
        {
            currentAbility = ability;
            UsageIndicator.fillAmount = 0;
            ResetCooldownIndicator();
            ability.onAbilityUse.AddListener(OnAbilityUse);
            ability.onCooldownEnter.AddListener(OnCooldownEnter);
        }

        void RemoveCurrentAbility()
        {
            if (cooldownCount != null) StopCoroutine(cooldownCount);
            currentAbility.onCooldownEnter.RemoveListener(OnCooldownEnter);
            currentAbility.onAbilityUse.RemoveListener(OnAbilityUse);
            currentAbility = null;
        }

        void OnAbilityUse()
        {
            if (cooldownCount != null) StopCoroutine(cooldownCount);
            UsageIndicator.fillAmount = 1;
            ResetCooldownIndicator();
        }

        void ResetCooldownIndicator()
        {
            cooldownIndicator.fillAmount = 0;
            cooldownTimer.text = "";
        }

        void OnCooldownEnter(float time)
        {
            if (cooldownCount != null) StopCoroutine(cooldownCount);
            if (isActiveAndEnabled) StartCoroutine(CooldownTimer(time));
            else UsageIndicator.fillAmount = 0;
        }

        IEnumerator CooldownTimer(float time)
        {
            UsageIndicator.fillAmount = 0;
            float cooldown = time;
            yield return new WaitWhile(() =>
            {
                cooldownIndicator.fillAmount = cooldown / time;
                cooldown -= Time.deltaTime;
                cooldownTimer.text = cooldown.ToString("F1");
                return cooldown > 0;
            });
            ResetCooldownIndicator();
        }
    }
}