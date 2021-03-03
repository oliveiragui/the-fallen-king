using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using Utils.MyBox.Extensions.EditorExtensions;
using Utils.MyBox.Tools;
#if UNITY_EDITOR

#endif

namespace Utils.MyBox.Attributes
{
    /// <summary>
    ///     Apply to MonoBehaviour field to assert that this field is assigned via inspector (not null, false, empty of zero)
    ///     on playmode
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MustBeAssignedAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    [InitializeOnLoad]
    public class MustBeAssignedAttributeChecker
    {
        /// <summary>
        ///     A way to conditionally disable MustBeAssigned check
        /// </summary>
        public static Func<FieldInfo, MonoBehaviour, bool> ExcludeFieldFilter;

        static MustBeAssignedAttributeChecker()
        {
            MyEditorEvents.OnSave += AssertComponentsInScene;
            PrefabStage.prefabSaved += AssertComponentsInPrefab;
        }

        static void AssertComponentsInScene()
        {
            var components = MyEditor.GetAllBehavioursInScenes();
            AssertComponent(components);
        }

        static void AssertComponentsInPrefab(GameObject prefab)
        {
            var components = prefab.GetComponentsInChildren<MonoBehaviour>();
            AssertComponent(components);
        }

        static void AssertComponent(MonoBehaviour[] components)
        {
            foreach (var behaviour in components)
            {
                if (behaviour == null) continue;
                var typeOfScript = behaviour.GetType();
                var mustBeAssignedFields = typeOfScript
                    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.IsDefined(typeof(MustBeAssignedAttribute), false)).ToArray();

                foreach (var field in mustBeAssignedFields)
                {
                    var propValue = field.GetValue(behaviour);

                    // Used by external systems to exclude specific fields.
                    // Specifically for ConditionalFieldAttribute
                    if (FieldIsExcluded(field, behaviour)) continue;

                    bool valueTypeWithDefaultValue = field.FieldType.IsValueType &&
                                                     Activator.CreateInstance(field.FieldType).Equals(propValue);
                    if (valueTypeWithDefaultValue)
                    {
                        Debug.LogError($"{typeOfScript.Name} caused: {field.Name} is Value Type with default value",
                            behaviour.gameObject);
                        continue;
                    }

                    bool nullReferenceType = propValue == null || propValue.Equals(null);
                    if (nullReferenceType)
                    {
                        Debug.LogError($"{typeOfScript.Name} caused: {field.Name} is not assigned (null value)",
                            behaviour.gameObject);
                        continue;
                    }

                    bool emptyString = field.FieldType == typeof(string) && (string) propValue == string.Empty;
                    if (emptyString)
                    {
                        Debug.LogError($"{typeOfScript.Name} caused: {field.Name} is not assigned (empty string)",
                            behaviour.gameObject);
                        continue;
                    }

                    var arr = propValue as Array;
                    bool emptyArray = arr != null && arr.Length == 0;
                    if (emptyArray)
                        Debug.LogError($"{typeOfScript.Name} caused: {field.Name} is not assigned (empty array)",
                            behaviour.gameObject);
                }
            }
        }

        static bool FieldIsExcluded(FieldInfo field, MonoBehaviour behaviour)
        {
            if (ExcludeFieldFilter == null) return false;

            foreach (var filterDelegate in ExcludeFieldFilter.GetInvocationList())
            {
                var filter = filterDelegate as Func<FieldInfo, MonoBehaviour, bool>;
                if (filter != null && filter(field, behaviour)) return true;
            }

            return false;
        }
    }

#endif
}