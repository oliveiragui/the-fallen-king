#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Utils.MyBox.Attributes
{
    [InitializeOnLoad]
    public class MustBeAssignedConditionalFieldExclude
    {
        static readonly Type _conditionallyVisibleType = typeof(ConditionalFieldAttribute);

        static MustBeAssignedConditionalFieldExclude()
        {
            MustBeAssignedAttributeChecker.ExcludeFieldFilter += ExcludeCheckIfConditionalFieldHidden;
        }

        static bool ExcludeCheckIfConditionalFieldHidden(FieldInfo field, MonoBehaviour behaviour)
        {
            if (_conditionallyVisibleType == null) return false;
            if (!field.IsDefined(_conditionallyVisibleType, false)) return false;

            // Get a specific attribute of this field
            var conditionalFieldAttribute = field.GetCustomAttributes(_conditionallyVisibleType, false)
                .Select(a => a as ConditionalFieldAttribute)
                .SingleOrDefault();

            return conditionalFieldAttribute != null &&
                   !ConditionalFieldUtility.BehaviourPropertyIsVisible(behaviour, field.Name,
                       conditionalFieldAttribute);
        }
    }
}
#endif