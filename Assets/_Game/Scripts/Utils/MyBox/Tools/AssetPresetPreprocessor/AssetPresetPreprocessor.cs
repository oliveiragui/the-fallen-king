#if UNITY_EDITOR
using System.Linq;
using _Game.Scripts.Utils.MyBox.Extensions.EditorExtensions;
using UnityEditor;

namespace _Game.Scripts.Utils.MyBox.Tools.AssetPresetPreprocessor
{
    public class AssetPresetPreprocessor : AssetPostprocessor
    {
        static AssetsPresetPreprocessBase _preprocessBase;
        static bool _preprocessBaseChecked;

        void OnPreprocessAsset()
        {
            if (!PreloadBase()) return;

            foreach (var preset in _preprocessBase.Presets)
            {
                if (preset.Preset == null) continue;
                if (!preset.Sample(assetPath)) continue;
                if (!preset.Preset.CanBeAppliedTo(assetImporter)) continue;

                preset.Preset.ApplyTo(assetImporter, preset.PropertiesToApply);
                return;
            }
        }

        bool PreloadBase()
        {
            if (_preprocessBaseChecked) return _preprocessBase != null;
            if (_preprocessBase == null)
            {
                _preprocessBase = MyScriptableObject.LoadAssetsFromResources<AssetsPresetPreprocessBase>()
                    .FirstOrDefault();
                if (_preprocessBase == null)
                    _preprocessBase = MyScriptableObject.LoadAssets<AssetsPresetPreprocessBase>().SingleOrDefault();

                _preprocessBaseChecked = true;
            }

            return _preprocessBase != null;
        }
    }
}
#endif