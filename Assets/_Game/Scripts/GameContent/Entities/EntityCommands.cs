using System.Collections;
using _Game.Scripts.Components.CombatSystem;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities
{
    public class EntityCommands : MonoBehaviour
    {
        public Entity entity;

        public bool CanReceiveMoveInput { get; set; } = true;

        #region Movement Commands

        
        public void MoveTo(Vector3 position)
        {
           
            if (CanReceiveMoveInput) entity.movement.MoveTo(position);
        }

        public void Stop()
        {
            if (CanReceiveMoveInput) entity.animations.StopRun();
        }

        public void TurnToLookDirection() =>
            entity.movement.Rotation = entity.lookDiretion;

        public void PlayFootStepSound() => entity.entityAudio.Play(entity.collision.FloorName);

        #endregion

        #region Ability Commands

        public void Die() => entity.animations.Die();

        public void UseAbility(Ability ability) => entity.animations.UseAbility(ability);

        public void StopCasting(Ability ability) => entity.animations.StopCasting(ability.AnimationId);

        public void EquipWeapon(Weapon weapon)
        {
            entity.animations.TrocaController(weapon);
            entity.mesh.SwitchWeapon(weapon.Prefabs);
        }

        #endregion
        
        #region To Refatory

        public void ApontaEnquantoConjura()
        {
            IEnumerator Aponta() => new WaitWhile(() =>
            {
                entity.transform.rotation = entity.lookDiretion;
                return entity.animations.conjuring;
            });

            StartCoroutine(Aponta());
        }

        void InvocaFlecha()
        {
            var tr = transform;
            var position = tr.position + new Vector3(0, 1, 0);
            var hit = new AbilityHit(-2, Vector3.zero, entity.associatedCharacter);
            var force = transform.forward.normalized * 800;

            entity.animations.weapon
                .ammoData
                .Instantiate(position, tr.rotation)
                .Shot(hit, force);
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
                if (!otherEntity.collision.Hittable) continue;
                if (otherEntity.associatedCharacter.Equals(entity.associatedCharacter)) continue;
                if (otherEntity.associatedCharacter.Team.PlayerFriend ==
                    entity.associatedCharacter.Team.PlayerFriend) continue;
                otherEntity.events.onHitReceived.Invoke(new AbilityHit(-2, Vector3.zero,
                    entity.associatedCharacter));
            }
        }

        # endregion
    }
}