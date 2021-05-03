using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.GameContent.Abilities
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] AbilityData data;
        public UnityEvent onAbilityUse = new UnityEvent();
        public CooldownEnterEvent onCooldownEnter = new CooldownEnterEvent();

        public AbilityCombo[] Combos;

        public AbilityData Data => data;

        public float Cooldown { get; private set; }

        public bool InCooldown { get; private set; }

        public int CurrentComboID { get; private set; }

        public bool Conjuring { get; private set; }

        public bool InUse { get; private set; }

        public AbilityCombo CurrentCombo => Combos[CurrentComboID];

        public Ability Setup(AbilityData data)
        {
            this.data = data;
            Combos = data.Combo.Select(comboData => new AbilityCombo(comboData, this)).ToArray();
            return this;
        }

        public ParticleSystem[] InstantiateParticles(Transform entityTransform)
        {
            var particleEffects = new ParticleSystem[data.ParticleEffects.Length];
            for (var i = 0; i < data.ParticleEffects.Length; i++)
                Instantiate(data.ParticleEffects[i].gameObject, entityTransform)
                    .TryGetComponent(out particleEffects[i]);

            return particleEffects;
        }

        public bool CanBeUsed => !InCooldown;

        public bool CanOverride(Ability other) => !other || Data.CanInterrupt && other.Data.CanBeInterruped;

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
            _cdCoroutine = StartCoroutine(CooldownTimer(data.Cooldown.Value));
        }

        Coroutine _cdCoroutine;

        IEnumerator CooldownTimer(float time)
        {
            CurrentComboID++;

            if (CurrentComboID < Combos.Length)
            {
                Cooldown = time;
                yield return new WaitForSeconds(1f);
            }

            CurrentComboID = 0;
            InCooldown = true;
            onCooldownEnter.Invoke(data.Cooldown.Value);
            yield return new WaitWhile(() =>
            {
                Cooldown -= Time.deltaTime;
                return Cooldown > 0;
            });
            InCooldown = false;
        }
    }

    [Serializable]
    public class CooldownEnterEvent : UnityEvent<float> { }
}