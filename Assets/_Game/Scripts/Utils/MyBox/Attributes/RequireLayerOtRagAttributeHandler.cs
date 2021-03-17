using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Utils.MyBox.Attributes
{
    [InitializeOnLoad]
    public class RequireLayerOtRagAttributeHandler
    {
        static RequireLayerOtRagAttributeHandler()
        {
            EditorApplication.playModeStateChanged += AutoSaveWhenPlaymodeStarts;
        }

        static void AutoSaveWhenPlaymodeStarts(PlayModeStateChange obj)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                var components = Object.FindObjectsOfType<Component>();
                foreach (var component in components)
                foreach (var attribute in component.GetType().GetCustomAttributes(true))
                {
                    var layerAttribute = attribute as RequireLayerAttribute;
                    if (layerAttribute != null)
                    {
                        int requiredLayer = layerAttribute.LayerName != null
                            ? LayerMask.NameToLayer(layerAttribute.LayerName)
                            : layerAttribute.LayerIndex;
                        if (component.gameObject.layer == requiredLayer) continue;

                        Debug.LogWarning("Layer of " + component.name + " changed by RequireLayerAttribute to " +
                                         layerAttribute.LayerName);
                        component.gameObject.layer = requiredLayer;
                        EditorUtility.SetDirty(component);

                        continue;
                    }

                    var tagAttribute = attribute as RequireTagAttribute;
                    if (tagAttribute != null)
                    {
                        if (component.CompareTag(tagAttribute.Tag)) continue;

                        Debug.LogWarning("Tag of " + component.name + " changed by RequireTagAttribute to " +
                                         tagAttribute.Tag);
                        component.gameObject.tag = tagAttribute.Tag;
                        EditorUtility.SetDirty(component);
                    }
                }
            }
        }
    }
}
#endif