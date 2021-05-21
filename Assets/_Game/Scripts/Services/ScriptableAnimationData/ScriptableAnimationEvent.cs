using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [CreateAssetMenu(fileName = "Scriptable Animation Event",
        menuName = "GameContent/Animation System/Scriptable Animation Event", order = 1)]
    public class ScriptableAnimationEvent : AnimationEventCreator
    {
        public string functionName;
        public int intParameter;
        public float floatParameter;
        public Object objectReferenceParameter;

        public override AnimationEvent CreateEvent() => new AnimationEvent
        {
            floatParameter = floatParameter,
            intParameter = intParameter,
            functionName = functionName,
            objectReferenceParameter = objectReferenceParameter
        };
    }
}