using Fluent;
using UnityEngine;

namespace _Game.Scripts
{
    public class IniciaDialogoPorEvento : MonoBehaviour
    {
        bool everythingOk = false;

        void Start()
        {
            if (GetComponent<FluentScript>() == null)
            {
                Debug.LogError("You need a FluentScript component on this object to initiate FluentDialogue", this);
                return;
            }
            everythingOk = true;
        }

        public void StartDialogue()
        {
            FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
        }
    }
}