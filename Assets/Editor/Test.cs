using Collections.Avatares.Componentes;

namespace Utils
{
    using UnityEditor;
    using UnityEngine;

    public class MenuTest : MonoBehaviour
    {
        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("CONTEXT/Rigidbody/Double Mass")]
        static void DoubleMass(MenuCommand command)
        {
            Rigidbody body = (Rigidbody) command.context;
            body.mass = body.mass * 2;
            Debug.Log("Doubled Rigidbody's Mass to " + body.mass + " from Context Menu.");
        }

        // Add a menu item to create custom GameObjects.
        // Priority 1 ensures it is grouped with the other menu items of the same kind
        // and propagated to the hierarchy dropdown and hierarchy context menus.
        [MenuItem("GameObject/Personagem/Mesh", false, 10)]
        static void CreateCustomGameObject(MenuCommand menuCommand)
        {
            var go = new GameObject("Personagem Mesh");
            go.AddComponent<AvatarMesh>();
            
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            
            Selection.activeObject = go;
        }
    }
}