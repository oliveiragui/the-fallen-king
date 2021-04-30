#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Utils.MyBox.Extensions.EditorExtensions
{
    public static class MyScriptableObject
    {
        /// <summary>
        ///     Load all ScriptableObjects of type
        /// </summary>
        public static T[] LoadAssetsFromResources<T>() where T : ScriptableObject =>
            Resources.FindObjectsOfTypeAll<T>();

        /// <summary>
        ///     Load all SO of type from Assets
        /// </summary>
        public static T[] LoadAssets<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            var a = new T[guids.Length];
            for (var i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }

        /// <summary>
        ///     Create ScriptableObject asset of name in folder
        /// </summary>
        public static T CreateAsset<T>(string name, string folder = "Assets") where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("ScriptableObjectUtility caused: Create Asset failed because Name is empty");
                return null;
            }

            string path = folder + "/" + name.Trim() + ".asset";

            var instance = ScriptableObject.CreateInstance<T>();

            string fullPath = Path.GetFullPath(path);
            string directory = Path.GetDirectoryName(fullPath);
            if (directory != null) Directory.CreateDirectory(directory);

            AssetDatabase.CreateAsset(instance, AssetDatabase.GenerateUniqueAssetPath(path));

            AssetDatabase.SaveAssets();

            Debug.Log("Scriptable object asset created at " + path);

            return instance;
        }

        public static T CreateAssetWithFolderDialog<T>(string filename) where T : ScriptableObject
        {
            string path = EditorUtility.SaveFolderPanel("Where to save", "Assets/", "");
            if (path.Length <= 0) return null;
            string relativePath = "Assets" + path.Substring(Application.dataPath.Length);

            return CreateAsset<T>(filename, relativePath);
        }
    }
}
#endif