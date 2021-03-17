using Entities.Common;
using UnityEngine;

namespace Entities.Default
{
    public class DefaultEntity : MonoBehaviour
    {
        [SerializeField] public EntityComponents components;
        [SerializeField] public EntityData data;
    }
}