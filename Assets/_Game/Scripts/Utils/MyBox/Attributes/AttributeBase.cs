using System;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Utils.MyBox.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class AttributeBase : PropertyAttribute
    {
#if UNITY_EDITOR
        /// <summary>
        ///     Validation is called before all other methods.
        ///     Once in OnGUI and once in GetPropertyHeight
        /// </summary>
        public virtual void ValidateProperty(SerializedProperty property) { }

        public virtual void OnBeforeGUI(Rect position, SerializedProperty property, GUIContent label) { }

        /// <summary>
        ///     Called once per AttributeBase group.
        ///     I.e. if something with higher order is drawn, later will be skipped
        /// </summary>
        /// <returns>false if nothing is drawn</returns>
        public virtual bool OnGUI(Rect position, SerializedProperty property, GUIContent label) => false;

        public virtual void OnAfterGUI(Rect position, SerializedProperty property, GUIContent label) { }

        /// <summary>
        ///     Overriding occurs just like OnGUI. Once per group, attribute with higher priority first
        /// </summary>
        /// <returns>Null if not overrided</returns>
        public virtual float? OverrideHeight() => null;
#endif
    }
}