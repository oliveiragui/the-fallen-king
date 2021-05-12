using System.Collections;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class CharacterHUD : MonoBehaviour
    {
        [SerializeField] Lifebar lifebar;
        Coroutine followingEntity;

        void Awake()
        {
            enabled = false;
        }

        public void UpdateLife(CharacterStatus characterStatus)
        {
            lifebar.Total = characterStatus.Life.Total;
            lifebar.Current = characterStatus.Life.Current;
        }

        public void FollowEntity(Entity entity)
        {
            StopFollowEntity(entity);
            followingEntity = StartCoroutine(Follow(entity));
        }

        IEnumerator Follow(Entity entity)
        {
            while (true)
            {
                transform.position = entity.transform.position;
                yield return null;
            }
        }

        public void StopFollowEntity(Entity entity)
        {
            if (followingEntity != null) StopCoroutine(followingEntity);
        }
    }
}