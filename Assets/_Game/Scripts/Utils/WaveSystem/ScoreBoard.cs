using TMPro;
using UnityEngine;

namespace _Game.Scripts.Utils
{
    public class ScoreBoard : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;

        [SerializeField] int totalPoint;

        public void ScorePoint(int value)
        {
            totalPoint += value;
            text.text = totalPoint.ToString();
        }
    }
}