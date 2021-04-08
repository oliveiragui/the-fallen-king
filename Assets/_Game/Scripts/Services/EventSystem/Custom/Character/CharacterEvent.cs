using System;
using _Game.Scripts.Components.EventSystem;
using _Game.Scripts.GameContent.Characters;
using UnityEngine;

namespace _Game.Scripts.Utils.Events
{
    [CreateAssetMenu(fileName = "New Character Event", menuName = "GameContent/Events/CharacterEvent")]
    public class CharacterEvent : GameEvent<Character>
    {
    }

    // [Serializable]
    // public class Character
    // {
    //     public int index;
    //     public int name;
    // }
}