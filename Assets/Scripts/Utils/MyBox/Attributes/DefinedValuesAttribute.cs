using System;
using UnityEditor;
using UnityEngine;

namespace Utils.MyBox.Attributes
{
	/// <summary>
	///     Create Popup with predefined values for string, int or float property
	/// </summary>
	public class DefinedValuesAttribute : PropertyAttribute
    {
        public readonly object[] ValuesArray;

        public DefinedValuesAttribute(params object[] definedValues)
        {
            ValuesArray = definedValues;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DefinedValuesAttribute))]
    public class DefinedValuesAttributeDrawer : PropertyDrawer
    {
        DefinedValuesAttribute _attribute;
        int _selectedValueIndex = -1;
        string[] _values;
        Type _variableType;

        bool IsString => _variableType == typeof(string);

        bool IsInt => _variableType == typeof(int);

        bool IsFloat => _variableType == typeof(float);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_attribute == null) Initialize(property);
            if (_values == null || _values.Length == 0 || _selectedValueIndex < 0)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            EditorGUI.BeginChangeCheck();
            _selectedValueIndex = EditorGUI.Popup(position, label.text, _selectedValueIndex, _values);
            if (EditorGUI.EndChangeCheck()) ApplyNewValue(property);
        }

        void Initialize(SerializedProperty property)
        {
            _attribute = (DefinedValuesAttribute) attribute;
            if (_attribute.ValuesArray == null || _attribute.ValuesArray.Length == 0) return;
            _variableType = _attribute.ValuesArray[0].GetType();
            if (TypeMismatch(property)) return;

            _values = new string[_attribute.ValuesArray.Length];
            for (int i = 0; i < _attribute.ValuesArray.Length; i++) _values[i] = _attribute.ValuesArray[i].ToString();

            _selectedValueIndex = GetSelectedIndex(property);
        }

        int GetSelectedIndex(SerializedProperty property)
        {
            for (int i = 0; i < _values.Length; i++)
            {
                if (IsString && property.stringValue == _values[i]) return i;
                if (IsInt && property.intValue == Convert.ToInt32(_values[i])) return i;
                if (IsFloat && Mathf.Approximately(property.floatValue, Convert.ToSingle(_values[i]))) return i;
            }

            return 0;
        }

        bool TypeMismatch(SerializedProperty property)
        {
            if (IsString && property.propertyType != SerializedPropertyType.String) return true;
            if (IsInt && property.propertyType != SerializedPropertyType.Integer) return true;
            if (IsFloat && property.propertyType != SerializedPropertyType.Float) return true;

            return false;
        }

        void ApplyNewValue(SerializedProperty property)
        {
            if (IsString) property.stringValue = _values[_selectedValueIndex];
            else if (IsInt) property.intValue = Convert.ToInt32(_values[_selectedValueIndex]);
            else if (IsFloat) property.floatValue = Convert.ToSingle(_values[_selectedValueIndex]);

            property.serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}