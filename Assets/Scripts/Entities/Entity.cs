using Abilities;
using Abilities.Collections.Habilidades;
using Ammo;
using Characters;
using CombatSystem;
using Entities.Animation;
using Entities.Audio;
using Entities.Mesh;
using Entities.Movement;
using Entities.Particle;
using Entities.PhysicsSystem;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] new EntityAnimation animation;
        [SerializeField] new EntityAudio audio;
        [SerializeField] EntityMesh mesh;
        [SerializeField] EntityParticle particle;
        [SerializeField] public EntityPhysics physics; // TODO: RETIRAR PÚBLICO
        [SerializeField] EntityMove movement;
        [SerializeField] EntityAbilityManager abilityManager;

        [SerializeField] public Character associatedCharacter;
        //public EntityPhysics Physics => physics;

        bool _inCombat;

        public EntityAbilityManager AbilityManager => abilityManager;
        public EntityCommands Comands { get; set; }

        public bool InCombat
        {
            get => _inCombat;
            set
            {
                _inCombat = value;
                mesh.InCombat = value;
                if (value) animation.EquipWeapon();
                else animation.UnequipWeapon();
            }
        }

        void Awake()
        {
            Comands = new EntityCommands(this);
        }

        public void EquipaArma(WeaponData weapon)
        {
            animation.TrocaController(weapon.AnimatorController);
            mesh.SwitchWeapon(weapon);
        }

        public void ProximoCombo(AbilityCombo combo, float attackSpeed)
        {
            animation.Ability.SetupCombo(combo, attackSpeed);
        }

        public void UsaHabilidade(AbilityData ability)
        {
            if (!AbilityManager.IsUsingAbility || abilityManager.CanSwitchAbility(ability))
            {
                animation.Ability.SetupAbility(ability);
                AbilityManager.currentAbility = ability;
            }

            if (AbilityManager.currentAbility.Id != ability.Id) return;
            animation.Ability.Use();
            animation.Ability.EntraEmCombate();
            InCombat = true;
        }

        public void ParaDeConjurar(int abilityID)
        {
            if (!AbilityManager.currentAbility) return;
            if (AbilityManager.currentAbility.Id == abilityID) animation.Ability.StopCasting();
        }

        public void Movimenta(float speed, float direction)
        {
            movement.Speed = speed;
            movement.Move(direction);
            animation.Run(1);
        }

        public void ParaDeAndar()
        {
            animation.StopRun();
            movement.Stop();
        }

        void EsferaDeDano()
        {
            var tr = transform;

            var hits = Physics.SphereCastAll(
                tr.position,
                2,
                tr.forward,
                0.01f,
                LayerMask.GetMask("Hittable"));

            foreach (var hit in hits)
            {
                if (!hit.collider.attachedRigidbody.transform.TryGetComponent(out Entity otherEntity)) continue;
                if (!otherEntity.physics.Hittable) continue;
                if (otherEntity.associatedCharacter.Equals(associatedCharacter)) continue;
                otherEntity.CauseDamage(new Damage(-2, Vector3.zero, associatedCharacter.Team));
            }
        }

        public void CauseDamage(Damage damage)
        {
            associatedCharacter.Status.Life.ApplyDamage(damage.value);
            // outroAvatar.Particulas.TocaParticulasDeSangue();
            // Avatar.Audio.TocaSom(SlotSom.GolpeDeEspada);
        }

        void InvocaFlecha()
        {
            AmmoStorage
                .Arrow(transform.position + new Vector3(0, 1, 0), transform.rotation)
                .Setup(new Damage(-2, Vector3.zero, associatedCharacter.Team), associatedCharacter, transform.forward.normalized*800);
        }
    }
}