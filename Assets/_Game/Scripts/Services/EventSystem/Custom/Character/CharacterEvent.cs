﻿using UnityEngine;

namespace _Game.Scripts.Services.EventSystem.Custom.Character
{
    [CreateAssetMenu(fileName = "New Character Event", menuName = "GameContent/Events/CharacterEvent")]
    public class CharacterEvent : GameEvent<GameModules.Characters.Scripts.Character> { }

    // [Serializable]
    // public class Character
    // {
    //     public int index;
    //     public int name;
    // }
}