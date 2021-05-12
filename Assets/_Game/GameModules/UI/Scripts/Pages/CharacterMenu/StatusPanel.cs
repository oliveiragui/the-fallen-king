using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu
{
    public class StatusPanel : MonoBehaviour
    {
        [SerializeField] Lifebar lifebar;
        [SerializeField] TextMeshProUGUI currentLifeValue;
        [SerializeField] TextMeshProUGUI lifeValue;
        [SerializeField] TextMeshProUGUI agilityValue;
        [SerializeField] TextMeshProUGUI forceValue;

        public void OnStatusChange(CharacterStatus characterStatus)
        {
            lifebar.Total = characterStatus.Life.Total;
            lifebar.Current = characterStatus.Life.Current;

            currentLifeValue.text = characterStatus.Life.Current.ToString();
            lifeValue.text = characterStatus.Life.Total.ToString();
            agilityValue.text = characterStatus.Agility.Current.ToString();
            forceValue.text = characterStatus.Strength.Current.ToString();
        }
    }
}