using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.GameContent.Entities.Components.Action
{
    [Serializable]
    public class EntityAction : MonoBehaviour
    {
        [SerializeField] EntityCommands entityCommands;

        [SerializeField] UnityEvent enter;
        [SerializeField] UnityEvent update;
        [SerializeField] UnityEvent fixedUpdate;
        [SerializeField] UnityEvent exit;

        void Awake()
        {
            enabled = false;
        }

        void Update()
        {
            update.Invoke();
        }

        void FixedUpdate()
        {
            fixedUpdate.Invoke();
        }

        public void SetEntity(Entity entity)
        {
            entityCommands.entity = entity;
        }

        public void SetCombatEntity(Entity entity)
        {
            entityCommands.entity = entity;
        }

        public void ActionEnter()
        {
            enabled = true;
            enter.Invoke();
        }

        public void ActionExit()
        {
            exit.Invoke();
            enabled = false;
        }
    }
}