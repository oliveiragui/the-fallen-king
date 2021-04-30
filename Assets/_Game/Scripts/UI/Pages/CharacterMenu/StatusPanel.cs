using _Game.Scripts.Services.AttributeSystem;
using _Game.Scripts.UI.Utils;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI.Pages.CharacterMenu
{
    public class StatusPanel : MonoBehaviour
    {
        [SerializeField] Lifebar lifebar;
        [SerializeField] TextMeshProUGUI currentLifeValue;
        [SerializeField] TextMeshProUGUI lifeValue;
        [SerializeField] TextMeshProUGUI speedValue;
        [SerializeField] TextMeshProUGUI agilityValue;
        [SerializeField] TextMeshProUGUI forceValue;

        public void OnStatusChange(Status status)
        {
            lifebar.Total = status.Life.Total;
            lifebar.Current = status.Life.Current;
            currentLifeValue.text = status.Life.Current.ToString();
            lifeValue.text = status.Life.Total.ToString();
            speedValue.text = status.Speed.Current.ToString();
            agilityValue.text = status.AttackSpeed.Current.ToString();
            forceValue.text = status.Strength.Current.ToString();
        }
    }
}