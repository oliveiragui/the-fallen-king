using System;
using System.Collections;
using _Game.GameModules.Characters.Scripts;
using UnityEngine;
using UnityEngine.Events;
using Attribute = _Game.Scripts.Services.AttributeSystem.Attribute;

namespace _Game.GameModules.Abilities.Scripts
{
    public class Ability : MonoBehaviour
    {
        Coroutine _cdCoroutine;

        [SerializeField] AbilityData data;

        public UnityEvent onAbilityUse = new UnityEvent();
        public CooldownEnterEvent onCooldownEnter = new CooldownEnterEvent();

        public AbilityData Data => data;
        public Attribute Cooldown;

        public Combo CurrentCombo => data.Combo[CurrentComboID];
        public int CurrentComboID { get; private set; }

        public bool Conjuring { get; private set; }
        public bool InUse { get; private set; }
        public bool OnCooldown { get; private set; }

        public bool CanBeUsed =>
            !OnCooldown && !InUse || !OnCooldown && InUse && CurrentComboID + 1 < Data.Combo.Length;

        public bool CanOverride(Ability other) => other && Data.CanOverride(other.Data);

        public Ability Setup(AbilityData data, CharacterStatus status)
        {
            this.data = data;
            Cooldown = new Attribute(data.Cooldown.Calculate(status.Agility));
            return this;
        }

        public void Use()
        {
            if (!CanBeUsed) return;
            Conjuring = true;
            InUse = true;
            if (!OnCooldown && _cdCoroutine != null) StopCoroutine(_cdCoroutine);
            onAbilityUse.Invoke();
        }

        public void StopConjuring()
        {
            Conjuring = false;
        }

        public void Finish()
        {
            InUse = false;
            if (_cdCoroutine != null) StopCoroutine(_cdCoroutine);
            _cdCoroutine = StartCoroutine(CooldownTimer());
        }

        IEnumerator CooldownTimer()
        {
            CurrentComboID++;
            if (CurrentComboID < Data.Combo.Length)
                yield return new WaitForSeconds(1f);
            CurrentComboID = 0;

            OnCooldown = true;
            onCooldownEnter.Invoke(data.Cooldown.Value);
            Cooldown.Current = Cooldown.Total;
            yield return new WaitWhile(() =>
            {
                Cooldown.Current -= Time.deltaTime;
                return Cooldown.Current > 0;
            });
            OnCooldown = false;
        }
    }

    [Serializable]
    public class CooldownEnterEvent : UnityEvent<float> { }
}