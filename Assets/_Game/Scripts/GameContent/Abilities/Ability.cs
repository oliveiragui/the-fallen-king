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
        ParticleSystem[] particleEffects;

        public AbilityData Data => data;

        public float Cooldown { get; private set; }

        public bool HaveParticleEffects => data.HaveParticles;

        public void PlayParticleEffect(int index)
        {
            particleEffects[index]?.Play();
        }

        public Ability Setup(AbilityData data)
        {
            this.data = data;
            Combos = data.Combo.Select(comboData => new AbilityCombo(comboData, this)).ToArray();
            return this;
        }

        public void InstantiateParticles(Transform entityTransform)
        {
            particleEffects = new ParticleSystem[data.ParticleEffects.Length];

            for (var i = 0; i < data.ParticleEffects.Length; i++)
            {
                Instantiate(data.ParticleEffects[i].gameObject, entityTransform)
                    .TryGetComponent(out particleEffects[i]);
            }
        }

        public void Initialize(Transform entityTransform)
        {
            if (HaveParticleEffects && particleEffects == null) InstantiateParticles(entityTransform);
        }

        public void Use()
        {
            onAbilityUse.Invoke();
        }

        public void Finish()
        {
            if (_cdCoroutine != null) StopCoroutine(_cdCoroutine);
            _cdCoroutine = StartCoroutine(CooldownTimer(data.Cooldown.Value));
            onCooldownEnter.Invoke(data.Cooldown.Value);
        }

        Coroutine _cdCoroutine;

        IEnumerator CooldownTimer(float time)
        {
            Cooldown = time;
            yield return new WaitWhile(() =>
            {
                Cooldown -= Time.deltaTime;
                return Cooldown > 0;
            });
        }
    }

    [Serializable]
    public class CooldownEnterEvent : UnityEvent<float> { }
}