using _Game.Scripts.Utils.Events;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.UI
{
    public class TabManager : MonoBehaviour
    {
        [SerializeField] UnityEvent onTabSwitch;
        [SerializeField] GameObject[] tabs;
        [SerializeField] int firstTab;

        public int OpenTabIndex { get; private set; }

        void Start()
        {
            OpenTabIndex = firstTab;
            ResetTab(OpenTabIndex);
        }

        void ResetTab(int index)
        {
            OpenTabIndex = index;
            foreach (var tab in tabs) tab.SetActive(false);
            tabs[OpenTabIndex].SetActive(true);
        }

        public void SwitchTab(int index)
        {
            tabs[OpenTabIndex].SetActive(false);
            OpenTabIndex = index;
            onTabSwitch.Invoke();
            tabs[OpenTabIndex].SetActive(true);
        }

        public void OpenNext()
        {
            int nextTabIndex = (OpenTabIndex < tabs.Length - 1) ? OpenTabIndex + 1 : 0;
            SwitchTab(nextTabIndex);
        }

        public void OpenPrevious()
        {
            int nextTabIndex = (OpenTabIndex > 0) ? OpenTabIndex - 1 : tabs.Length - 1;
            SwitchTab(nextTabIndex);
        }

        void OnEnable()
        {
            SwitchTab(firstTab);
        }
    }
}