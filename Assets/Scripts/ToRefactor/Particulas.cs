using System.Collections.Generic;
using UnityEngine;

namespace ToRefactor
{
    [CreateAssetMenu(fileName = "Particula", menuName = "Colecoes/Particulas", order = 0)]
    public class Particulas : ScriptableObject
    {
        Dictionary<string, GameObject> sons;

        public GameObject Som(string audioName)
        {
            return sons[audioName];
        }
    }
}