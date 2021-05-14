using System;
using System.Collections;
using System.Linq;
using _Game.GameModules.Characters.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.GameModules.Abilities.Scripts
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] AbilityData data;

        public UnityEvent onAbilityUse = new UnityEvent();
        public CooldownEnterEvent onCooldownEnter = new CooldownEnterEvent();

        Coroutine _cdCoroutine;

        public AbilityData Data => data;

        public float TotalCooldown { get; private set; }
        public float RemainingCooldownTime { get; private set; }

        public Combo CurrentCombo => data.Combo[CurrentComboID];
        public int CurrentComboID { get; private set; }

        public bool Conjuring { get; private set; }
        public bool InUse { get; private set; }
        public bool InCooldown { get; private set; }
        public bool CanBeUsed => !InCooldown && !Blocked;
        public bool Blocked { get; set; }

        public bool CanOverride(Ability other) => !other || Data.CanOverride(other.Data);

        public Ability Setup(AbilityData data, CharacterStatus status)
        {
            this.data = data;
            TotalCooldown = data.Cooldown.Calculate(status.Agility);

            return this;
        }

        public void Use()
        {
            if (!CanBeUsed) return;
            onAbilityUse.Invoke();
            Conjuring = true;
            InUse = true;
            Blocked = true;
            if (!InCooldown && _cdCoroutine != null) StopCoroutine(_cdCoroutine);
        }

        public void StopConjuring()
        {
            Conjuring = false;
        }

        public void Finish()
        {
            InUse = false;
            // InCooldown = true;
            if (_cdCoroutine != null) StopCoroutine(_cdCoroutine);
            _cdCoroutine = StartCoroutine(CooldownTimer());
        }

        IEnumerator CooldownTimer()
        {
            yield return WaitComboTransitionTime();
            yield return WaitCooldown();
        }

        IEnumerator WaitCooldown()
        {
            InCooldown = true;
            onCooldownEnter.Invoke(data.Cooldown.Value);
            RemainingCooldownTime = TotalCooldown;
            yield return new WaitWhile(() =>
            {
                RemainingCooldownTime -= Time.deltaTime;
                return RemainingCooldownTime > 0;
            });
            InCooldown = false;
        }

        IEnumerator WaitComboTransitionTime()
        {
            CurrentComboID++;
            if (CurrentComboID < Data.Combo.Length)
                yield return new WaitForSeconds(1f);
            CurrentComboID = 0;
        }
    }

    [Serializable]
    public class CooldownEnterEvent : UnityEvent<float> { }
}