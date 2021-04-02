using System.Collections;
using _Game.Scripts.Components;
using _Game.Scripts.UI.StatusBar;
using _Game.Scripts.Utils.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.UI
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] GameData data;
        [SerializeField] ResizableBar bar;

        public static void Load()
        {
            SceneManager.LoadScene("LoadScene");
        }

        void Start()
        {
            StartCoroutine(LoadYourAsyncScene(data.RequestedSceneIndex));
        }

        IEnumerator LoadYourAsyncScene(int index)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(index);
            yield return new WaitWhile(() =>
            {
                bar.ApplyVariation(asyncLoad.progress - 0.1f);
                Debug.Log(normalize(asyncLoad.progress * 1.1f));
                return !asyncLoad.isDone;
            });
        }

        public float normalize(float value)
        {
            if (value > 1) return 1;
            return value;
        }
    }
}