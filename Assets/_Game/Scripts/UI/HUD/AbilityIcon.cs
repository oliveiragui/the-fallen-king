using System.Collections;
using _Game.Scripts.GameContent.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.HUD
{
    public class AbilityIcon : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI buttonIcon;

        [SerializeField] Image UsageIndicator;

        [SerializeField] TextMeshProUGUI cooldownTimer;
        [SerializeField] Image cooldownIndicator;

        Coroutine cooldownCount;

        Ability currentAbility;

        public void BindAbility(Ability ability, string buttonName)
        {
            buttonIcon.text = $"<sprite=\"XboxOne\" name=\"XboxOne_{buttonName}\">";
            icon.sprite = ability.Data.Icon;

            if (currentAbility)
            {
                if (cooldownCount != null) StopCoroutine(cooldownCount);
                currentAbility.onCooldownEnter.RemoveListener(OnCooldownEnter);
                ability.onAbilityUse.RemoveListener(OnAbilityUse);
            }

            currentAbility = ability;
            UsageIndicator.fillAmount = 0;
            ResetCooldownIndicator();

            ability.onAbilityUse.AddListener(OnAbilityUse);
            ability.onCooldownEnter.AddListener(OnCooldownEnter);
        }

        void OnAbilityUse()
        {
            if (cooldownCount != null) StopCoroutine(cooldownCount);
            UsageIndicator.fillAmount = 1;
            ResetCooldownIndicator();
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

        void ResetCooldownIndicator()
        {
            cooldownIndicator.fillAmount = 0;
            cooldownTimer.text = "";
        }
    }
}