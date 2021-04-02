#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor.Presets;
using UnityEngine;

namespace _Game.Scripts.Utils.MyBox.Tools.AssetPresetPreprocessor
{
    public class AssetsPresetPreprocessBase : ScriptableObject
    {
        public ConditionalPreset[] Presets;

        public string[] ExcludeProperties = {"SpriteBorder", "Pivot", "Alignment"};
    }

    [Serializable]
    public class ConditionalPreset
    {
        public string PathContains;
        public string TypeOf;
        public string Prefix;
        public string Postfix;

        public Preset Preset;

        public string[] PropertiesToApply;

        public bool Sample(string path)
        {
            bool pathSet = !string.IsNullOrEmpty(PathContains);
            bool typeSet = !string.IsNullOrEmpty(TypeOf);
            bool prefixSet = !string.IsNullOrEmpty(Prefix);
            bool postfixSet = !string.IsNullOrEmpty(Postfix);

            if (pathSet && !path.Contains(PathContains)) return false;

            string extension = Path.GetExtension(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            if (extension == null || filename == null) return false;

            if (typeSet && !extension.Contains(TypeOf)) return false;

            if (prefixSet && !filename.StartsWith(Prefix)) return false;
            if (postfixSet && !filename.EndsWith(Postfix)) return false;

            return true;
        }
    }
}
#endif