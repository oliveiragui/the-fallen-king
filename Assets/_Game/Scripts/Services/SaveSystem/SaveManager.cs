using UnityEngine;

namespace _Game.Scripts
{
    public class SaveManager : MonoBehaviour
    {
        public static void SaveData<T>(string key, T data)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public static T LoadData<T>(string key) => JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));

        public static void LoadDataTo<T>(string key, T destination) =>
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), destination);
    }
}