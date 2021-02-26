using Collection.Entities.Animation;
using Collection.Entities.Audio;
using Collection.Entities.Mesh;
using Collection.Entities.Particle;
using Collection.Entities.Physics;
using Collection.Abilities.Collections.Habilidades;
using Collection.Weapons;
using Components.Move;
using UnityEngine;

namespace Collection.Entities
{
    public class Entity : MonoBehaviour
    {
        bool _inCombat;

        [SerializeField] new EntityAnimation animation;
        [SerializeField] new EntityAudio audio;
        [SerializeField] EntityMesh mesh;
        [SerializeField] EntityParticle particle;
        [SerializeField] EntityPhysics physics;
        [SerializeField] EntityMove movement;
        [SerializeField] EntityAbilityManager abilityManager;

        public EntityAbilityManager AbilityManager => abilityManager;

        public void EquipaArma(WeaponModel weapon)
        {
            animation.TrocaController(weapon.AnimatorController);
            mesh.SwitchWeapon(weapon);
        }

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
        

        public void ProximoCombo(AbilityCombo combo, float attackSpeed)
        {
            animation.Ability.SetupCombo(combo, attackSpeed);
        }

        public void UsaHabilidade(AbilityModel ability)
        {
            if (!AbilityManager.IsUsingAbility || abilityManager.CanSwitchAbility(ability))
            {
                animation.Ability.SetupAbility(ability);
                AbilityManager.currentAbility = ability;
            }

            animation.Ability.Use();
        }

        public void ParaDeConjurar(int abilityID)
        {
            animation.Ability.StopCasting(abilityID);
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
    }
}