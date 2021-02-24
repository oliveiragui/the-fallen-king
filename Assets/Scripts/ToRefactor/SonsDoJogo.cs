using System.Collections.Generic;
using UnityEngine;

namespace ToRefactor
{
    [CreateAssetMenu(fileName = "SonsDoJogo", menuName = "SonsDoJogo", order = 0)]
    public class SonsDoJogo : ScriptableObject
    {
        [SerializeField] Dictionary<string, AudioClip> sons;

        public AudioClip Som(string audioName)
        {
            return sons[audioName];
        }
    }
}