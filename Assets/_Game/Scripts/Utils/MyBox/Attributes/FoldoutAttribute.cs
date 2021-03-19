﻿// ---------------------------------------------------------------------------- 
// Author: Dimitry, PixeyeHQ
// Project : UNITY FOLDOUT
// https://github.com/PixeyeHQ/InspectorFoldoutGroup
// Contacts : Pix - ask@pixeye.games
// Website : http://www.pixeye.games
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.MyBox.Attributes
{
    public class FoldoutAttribute : PropertyAttribute
    {
        public readonly bool foldEverything;
        public readonly string name;

        /// <summary>Adds the property to the specified foldout group.</summary>
        /// <param name="name">Name of the foldout group.</param>
        /// <param name="foldEverything">Toggle to put all properties to the specified group</param>
        public FoldoutAttribute(string name, bool foldEverything = false)
        {
            this.foldEverything = foldEverything;
            this.name = name;
        }
    }

#if UNITY_EDITOR
    public class FoldoutAttributeHandler
    {
        readonly Dictionary<string, CacheFoldProp> cacheFolds = new Dictionary<string, CacheFoldProp>();
        readonly List<SerializedProperty> props = new List<SerializedProperty>();
        readonly SerializedObject serializedObject;

        readonly Object target;
        bool initialized;

        //===============================//
        // Logic
        //===============================//

        public FoldoutAttributeHandler(Object target, SerializedObject serializedObject)
        {
            this.target = target;
            this.serializedObject = serializedObject;
        }

        public bool OverrideInspector => props.Count > 0;

        public void OnDisable()
        {
            if (target == null) return;

            foreach (var c in cacheFolds)
            {
                EditorPrefs.SetBool(string.Format($"{c.Value.atr.name}{c.Value.props[0].name}{target.name}"),
                    c.Value.expanded);
                c.Value.Dispose();
            }
        }

        public void Update()
        {
            serializedObject.Update();
            Setup();
        }

        public void OnInspectorGUI()
        {
            Header();
            Body();

            serializedObject.ApplyModifiedProperties();
        }

        void Header()
        {
            using (new EditorGUI.DisabledScope("m_Script" == props[0].propertyPath))
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(props[0], true);
                EditorGUILayout.Space();
            }
        }

        void Body()
        {
            foreach (var pair in cacheFolds)
            {
                EditorGUILayout.BeginVertical(StyleFramework.Box);
                Foldout(pair.Value);
                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel = 0;
            }

            EditorGUILayout.Space();

            for (int i = 1; i < props.Count; i++) EditorGUILayout.PropertyField(props[i], true);

            EditorGUILayout.Space();
        }

        void Foldout(CacheFoldProp cache)
        {
            cache.expanded = EditorGUILayout.Foldout(cache.expanded, cache.atr.name, true,
                StyleFramework.Foldout);

            if (cache.expanded)
            {
                EditorGUI.indentLevel = 1;

                for (int i = 0; i < cache.props.Count; i++)
                {
                    EditorGUILayout.BeginVertical(StyleFramework.BoxChild);
                    EditorGUILayout.PropertyField(cache.props[i],
                        new GUIContent(cache.props[i].name.FirstLetterToUpperCase()), true);
                    EditorGUILayout.EndVertical();
                }
            }
        }

        void Setup()
        {
            if (initialized) return;

            List<FieldInfo> objectFields;
            FoldoutAttribute prevFold = default;

            int length = EditorTypes.Get(target, out objectFields);

            for (int i = 0; i < length; i++)
            {
                #region FOLDERS

                var fold = Attribute.GetCustomAttribute(objectFields[i], typeof(FoldoutAttribute)) as FoldoutAttribute;
                CacheFoldProp c;
                if (fold == null)
                {
                    if (prevFold != null && prevFold.foldEverything)
                    {
                        if (!cacheFolds.TryGetValue(prevFold.name, out c))
                            cacheFolds.Add(prevFold.name,
                                new CacheFoldProp {atr = prevFold, types = new HashSet<string> {objectFields[i].Name}});
                        else
                            c.types.Add(objectFields[i].Name);
                    }

                    continue;
                }

                prevFold = fold;

                if (!cacheFolds.TryGetValue(fold.name, out c))
                {
                    bool expanded =
                        EditorPrefs.GetBool(string.Format($"{fold.name}{objectFields[i].Name}{target.name}"), false);
                    cacheFolds.Add(fold.name,
                        new CacheFoldProp
                            {atr = fold, types = new HashSet<string> {objectFields[i].Name}, expanded = expanded});
                }
                else
                {
                    c.types.Add(objectFields[i].Name);
                }

                #endregion
            }

            var property = serializedObject.GetIterator();
            bool next = property.NextVisible(true);
            if (next)
                do
                {
                    HandleFoldProp(property);
                } while (property.NextVisible(false));

            initialized = true;
        }

        void HandleFoldProp(SerializedProperty prop)
        {
            bool shouldBeFolded = false;

            foreach (var pair in cacheFolds)
                if (pair.Value.types.Contains(prop.name))
                {
                    var pr = prop.Copy();
                    shouldBeFolded = true;
                    pair.Value.props.Add(pr);

                    break;
                }

            if (shouldBeFolded == false)
            {
                var pr = prop.Copy();
                props.Add(pr);
            }
        }

        class CacheFoldProp
        {
            public readonly List<SerializedProperty> props = new List<SerializedProperty>();
            public FoldoutAttribute atr;
            public bool expanded;
            public HashSet<string> types = new HashSet<string>();

            public void Dispose()
            {
                props.Clear();
                types.Clear();
                atr = null;
            }
        }
    }

    internal static class StyleFramework
    {
        public static readonly GUIStyle Box;
        public static readonly GUIStyle BoxChild;
        public static readonly GUIStyle Foldout;

        static StyleFramework()
        {
            bool pro = EditorGUIUtility.isProSkin;

            var uiTex_in = Resources.Load<Texture2D>("IN foldout focus-6510");
            var uiTex_in_on = Resources.Load<Texture2D>("IN foldout focus on-5718");

            var c_on = pro ? Color.white : new Color(51 / 255f, 102 / 255f, 204 / 255f, 1);

            Foldout = new GUIStyle(EditorStyles.foldout);

            Foldout.overflow = new RectOffset(-10, 0, 3, 0);
            Foldout.padding = new RectOffset(25, 0, -3, 0);

            Foldout.active.textColor = c_on;
            Foldout.active.background = uiTex_in;
            Foldout.onActive.textColor = c_on;
            Foldout.onActive.background = uiTex_in_on;

            Foldout.focused.textColor = c_on;
            Foldout.focused.background = uiTex_in;
            Foldout.onFocused.textColor = c_on;
            Foldout.onFocused.background = uiTex_in_on;

            Foldout.hover.textColor = c_on;
            Foldout.hover.background = uiTex_in;

            Foldout.onHover.textColor = c_on;
            Foldout.onHover.background = uiTex_in_on;

            Box = new GUIStyle(GUI.skin.box);
            Box.padding = new RectOffset(10, 0, 10, 0);

            BoxChild = new GUIStyle(GUI.skin.box);
            BoxChild.active.textColor = c_on;
            BoxChild.active.background = uiTex_in;
            BoxChild.onActive.textColor = c_on;
            BoxChild.onActive.background = uiTex_in_on;

            BoxChild.focused.textColor = c_on;
            BoxChild.focused.background = uiTex_in;
            BoxChild.onFocused.textColor = c_on;
            BoxChild.onFocused.background = uiTex_in_on;

            EditorStyles.foldout.active.textColor = c_on;
            EditorStyles.foldout.active.background = uiTex_in;
            EditorStyles.foldout.onActive.textColor = c_on;
            EditorStyles.foldout.onActive.background = uiTex_in_on;

            EditorStyles.foldout.focused.textColor = c_on;
            EditorStyles.foldout.focused.background = uiTex_in;
            EditorStyles.foldout.onFocused.textColor = c_on;
            EditorStyles.foldout.onFocused.background = uiTex_in_on;

            EditorStyles.foldout.hover.textColor = c_on;
            EditorStyles.foldout.hover.background = uiTex_in;

            EditorStyles.foldout.onHover.textColor = c_on;
            EditorStyles.foldout.onHover.background = uiTex_in_on;
        }

        public static string FirstLetterToUpperCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static IList<Type> GetTypeTree(this Type t)
        {
            var types = new List<Type>();
            while (t.BaseType != null)
            {
                types.Add(t);
                t = t.BaseType;
            }

            return types;
        }
    }

    internal static class EditorTypes
    {
        public static Dictionary<int, List<FieldInfo>> fields =
            new Dictionary<int, List<FieldInfo>>(FastComparable.Default);

        public static int Get(object target, out List<FieldInfo> objectFields)
        {
            var t = target.GetType();
            int hash = t.GetHashCode();

            if (!fields.TryGetValue(hash, out objectFields))
            {
                var typeTree = t.GetTypeTree();
                objectFields = target.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                               BindingFlags.NonPublic | BindingFlags.NonPublic)
                    .OrderByDescending(x => typeTree.IndexOf(x.DeclaringType))
                    .ToList();
                fields.Add(hash, objectFields);
            }

            return objectFields.Count;
        }
    }

    internal class FastComparable : IEqualityComparer<int>
    {
        public static FastComparable Default = new FastComparable();

        public bool Equals(int x, int y)
        {
            return x == y;
        }

        public int GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }

#endif
}