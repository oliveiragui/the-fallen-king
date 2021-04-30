﻿using System.Collections;
using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.Services.AttributeSystem;
using _Game.Scripts.UI.Utils;
using UnityEngine;

namespace _Game.Scripts.UI.HUD
{
    public class CharacterHUD : MonoBehaviour
    {
        [SerializeField] Lifebar lifebar;
        Coroutine followingEntity;

        void Awake()
        {
            enabled = false;
        }

        public void UpdateLife(Status status)
        {
            lifebar.Total = status.Life.Total;
            lifebar.Current = status.Life.Current;
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