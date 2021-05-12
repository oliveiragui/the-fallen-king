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
        Timer cooldownTimer = new Timer();

        public UnityEvent onAbilityUse = new UnityEvent();
        public CooldownEnterEvent onCooldownEnter = new CooldownEnterEvent();

        Coroutine _cdCoroutine;

        public AbilityData Data => data;

        public float Cooldown { get; private set; }

        public Combo CurrentCombo => data.Combo[CurrentComboID];
        public int CurrentComboID { get; private set; }

        public bool Conjuring { get; private set; }
        public bool InUse { get; private set; }
        public bool InCooldown => cooldownTimer.IsRunning;
        public bool CanBeUsed => !cooldownTimer.IsRunning;
        public float RemainingCooldown => cooldownTimer.RemainingTime;

        public bool CanOverride(Ability other) => !other || Data.CanOverride(other.Data);
        
        public Ability Setup(AbilityData data) 
        {
            this.data = data;
            return this;
        }

        public void Use()
        {
            onAbilityUse.Invoke();
            Conjuring = true;
            InUse = true;
            if (!InCooldown && _cdCoroutine != null) StopCoroutine(_cdCoroutine);
        }

        public void StopConjuring()
        {
            Conjuring = false;
        }

        public void Finish()
        {
            InUse = false;
            if (_cdCoroutine != null) StopCoroutine(_cdCoroutine);
            _cdCoroutine = StartCoroutine(CooldownTimer(Cooldown));
        }

        IEnumerator CooldownTimer(float time)
        {
            yield return WaitComboTransitionTime();
            onCooldownEnter.Invoke(time);
            yield return cooldownTimer.StartTimer(time);
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
    public class Timer
    {
        float remainingTime;

        public bool IsRunning { get; private set; }
        public float RemainingTime => remainingTime;

        public IEnumerator StartTimer(float time)
        {
            IsRunning = true;
            remainingTime = time;
            yield return new WaitWhile(() =>
            {
                remainingTime -= Time.deltaTime;
                return remainingTime > 0;
            });
            IsRunning = false;
        }
    }

    [Serializable]
    public class CooldownEnterEvent : UnityEvent<float> { }
}