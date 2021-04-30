using System.Collections;
using _Game.Scripts.Services;
using _Game.Scripts.UI.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.UI
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] GameData data;
        [SerializeField] ResizableBar bar;

        void Start()
        {
            StartCoroutine(LoadYourAsyncScene(data.RequestedSceneIndex));
        }

        public static void Load()
        {
            SceneManager.LoadScene("LoadScene");
        }

        IEnumerator LoadYourAsyncScene(int index)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(index);
            yield return new WaitWhile(() =>
            {
                bar.ApplyVariation(asyncLoad.progress - 0.1f);
                //Debug.Log(normalize(asyncLoad.progress * 1.1f));
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