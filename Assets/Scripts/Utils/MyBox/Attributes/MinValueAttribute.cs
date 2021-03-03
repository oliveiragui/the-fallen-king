using UnityEditor;
using UnityEngine;
using Utils.MyBox.Extensions.EditorExtensions;
#if UNITY_EDITOR

#endif

namespace Utils.MyBox.Attributes
{
    public class MinValueAttribute : AttributeBase
    {
        readonly bool _vectorValuesSet;
        readonly float _x;
        readonly float _y;
        readonly float _z;

        public MinValueAttribute(float value)
        {
            _x = value;
        }

        public MinValueAttribute(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
            _vectorValuesSet = true;
        }

#if UNITY_EDITOR
        string _warning;

        public override void ValidateProperty(SerializedProperty property)
        {
            if (!property.IsNumerical())
            {
                if (_warning == null)
                    _warning = property.name + " caused: [MinValueAttribute] used with non-numeric property";
                return;
            }

            bool valueHandled = HandleValues(property);
            if (valueHandled) property.serializedObject.ApplyModifiedProperties();
        }

        public override bool OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_warning == null) return false;
            EditorGUI.HelpBox(position, _warning, MessageType.Warning);
            return true;
        }

        public override float? OverrideHeight()
        {
            if (_warning == null) return null;
            return EditorGUIUtility.singleLineHeight;
        }

        #region Handle Value

        /// <returns>true if fixed</returns>
        bool HandleValues(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                case SerializedPropertyType.Integer:
                    return HandleNumerics(property);

                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                case SerializedPropertyType.Vector4:
                    return HandleVectors(property);

                case SerializedPropertyType.Vector2Int:
                case SerializedPropertyType.Vector3Int:
                    return HandleIntVectors(property);
            }

            return false;
        }

        bool HandleNumerics(SerializedProperty property)
        {
            float minValue = _x;

            if (property.propertyType == SerializedPropertyType.Integer && property.intValue < minValue)
            {
                property.intValue = (int) minValue;
                return true;
            }

            if (property.propertyType == SerializedPropertyType.Float && property.floatValue < minValue)
            {
                property.floatValue = minValue;
                return true;
            }

            return false;
        }

        bool HandleVectors(SerializedProperty property)
        {
            float x = _x;
            float y = _vectorValuesSet ? _y : x;
            float z = _vectorValuesSet ? _z : x;

            var vector = Vector4.zero;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    vector = property.vector2Value;
                    break;
                case SerializedPropertyType.Vector3:
                    vector = property.vector3Value;
                    break;
                case SerializedPropertyType.Vector4:
                    vector = property.vector4Value;
                    break;
            }

            bool handled = false;
            if (vector[0] < x)
            {
                vector[0] = x;
                handled = true;
            }

            if (vector[1] < y)
            {
                vector[1] = y;
                handled = true;
            }

            if (vector[2] < z)
            {
                vector[2] = z;
                handled = true;
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    property.vector2Value = vector;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = vector;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = vector;
                    break;
            }

            return handled;
        }

        bool HandleIntVectors(SerializedProperty property)
        {
            int x = (int) _x;
            int y = _vectorValuesSet ? (int) _y : x;
            int z = _vectorValuesSet ? (int) _z : x;

            if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                var vector = property.vector2IntValue;
                if (vector.x < x || vector.y < y)
                {
                    property.vector2IntValue = new Vector2Int(
                        vector.x < x ? x : vector.x,
                        vector.y < y ? y : vector.y);
                    return true;
                }

                return false;
            }

            if (property.propertyType == SerializedPropertyType.Vector3Int)
            {
                var vector = property.vector3IntValue;
                if (vector.x < x || vector.y < y || vector.z < z)
                {
                    property.vector3IntValue = new Vector3Int(
                        vector.x < x ? x : vector.x,
                        vector.y < y ? y : vector.y,
                        vector.z < z ? z : vector.z);
                    return true;
                }

                return false;
            }

            return false;
        }

        #endregion

#endif
    }
}