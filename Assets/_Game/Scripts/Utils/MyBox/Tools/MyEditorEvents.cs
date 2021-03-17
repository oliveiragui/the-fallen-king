#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Utils.MyBox.Tools
{
    [InitializeOnLoad]
    public class MyEditorEvents : UnityEditor.AssetModificationProcessor, IPreprocessBuildWithReport
    {
        /// <summary>
        ///     Occurs on Scenes/Assets Save
        /// </summary>
        public static Action OnSave;

        /// <summary>
        ///     Occurs on first frame in Playmode
        /// </summary>
        public static Action OnFirstFrame;

        public static Action BeforePlaymode;

        public static Action BeforeBuild;

        static MyEditorEvents()
        {
            EditorApplication.update += CheckOnce;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        /// <summary>
        ///     On Editor Save
        /// </summary>
        static string[] OnWillSaveAssets(string[] paths)
        {
            // Prefab creation enforces SaveAsset and this may cause unwanted dir cleanup
            if (paths.Length == 1 && (paths[0] == null || paths[0].EndsWith(".prefab"))) return paths;

            if (OnSave != null) OnSave();

            return paths;
        }

        /// <summary>
        ///     Before Build
        /// </summary>
        public void OnPreprocessBuild(BuildReport report)
        {
            if (BeforeBuild != null) BeforeBuild();
        }

        public int callbackOrder => 0;

        /// <summary>
        ///     On First Frame
        /// </summary>
        static void CheckOnce()
        {
            if (Application.isPlaying)
            {
                EditorApplication.update -= CheckOnce;
                if (OnFirstFrame != null) OnFirstFrame();
            }
        }

        /// <summary>
        ///     On Before Playmode
        /// </summary>
        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode && BeforePlaymode != null) BeforePlaymode();
        }
    }
}
#endif