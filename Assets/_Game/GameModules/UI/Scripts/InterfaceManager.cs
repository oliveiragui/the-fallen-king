using System;
using _Game.GameModules.UI.Scripts.Utils;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts
{
    public class InterfaceManager : TabManager
    {
        [SerializeField] GameObject HUD;
        [SerializeField] bool playerCanSwitchMenu;
        [SerializeField] MenuInputData mainMenu;
        [SerializeField] MenuInputData charMenu;
        

        public bool PlayerCanSwitchMenu
        {
            get => playerCanSwitchMenu;
            set => playerCanSwitchMenu = value;
        }

        void Update()
        {
            ProcessInput();
        }

        public void ProcessInput()
        {
            if (!playerCanSwitchMenu) return;
            if (Input.GetButtonDown(mainMenu.ButtonName) && mainMenu.canBeUsed) SwitchMenu(mainMenu.Index);
        }

        public void SwitchMenu(int menuIndex)
        {
            if (OpenTabIndex == menuIndex) SwitchTab(0);
            else SwitchTab(menuIndex);
        }

        public void HideHUD(bool value)
        {
            HUD.SetActive(value);
        }
    }

    [Serializable]
    public class MenuInputData
    {
        [SerializeField] string buttonName = "";
        [SerializeField] int index;
        public bool canBeUsed;

        public string ButtonName => buttonName;
        public int Index => index;
    }
}