using UnityEngine;

namespace _Game.GameModules.Characters.Scripts
{
    public class CharacterPivotController: MonoBehaviour
    {
        [SerializeField] Character character;
        [SerializeField] GameObject pivot;
        [SerializeField] GameObject controller;

        public void UsePivot(bool value)
        {
            character.Entity.gameObject.SetActive(!value);
            pivot.SetActive(value);
            controller.SetActive(!value);
        }
    }
}