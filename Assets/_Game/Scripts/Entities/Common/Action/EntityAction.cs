using System;
using Entities.Combat;
using Entities.Default;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Common.Action
{
    [Serializable]
    public class EntityAction : MonoBehaviour
    {
        [SerializeField] DefaultCommands defaultCommands;
        [SerializeField] CombatCommands combatCommands;

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

        public void SetEntity(DefaultEntity entity)
        {
            defaultCommands.entity = entity;
        }

        public void SetCombatEntity(CombatEntity entity)
        {
            combatCommands.entity = entity;
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