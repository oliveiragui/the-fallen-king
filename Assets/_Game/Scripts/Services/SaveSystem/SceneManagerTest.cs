using UnityEngine;

namespace _Game.Scripts
{
    public class SceneManagerTest : MonoBehaviour
    {
        [SerializeField] int valorTotal;

        void Start()
        {
            SaveManager.LoadDataTo("SceneManagerTest", this);
            SaveManager.SaveData("SceneManagerTest", this);
        }
    }
}