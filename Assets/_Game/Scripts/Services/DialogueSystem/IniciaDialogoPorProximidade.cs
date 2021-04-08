using Fluent;
using UnityEngine;

namespace _Game.Scripts
{

    [RequireComponent(typeof(Collider))]
    public class IniciaDialogoPorProximidade : GameActionInitiator
    {
        public GameObject PlayerGameObject;
        bool over = false;
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

        void Update()
        {
            if (!everythingOk)
                return;

            if (over && Input.GetKeyDown(KeyCode.E))
            {
                FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
            }

        }

        void OnTriggerEnter(Collider collider)
        {
           
            if (collider.attachedRigidbody.gameObject == PlayerGameObject)
            {
                FluentManager.Instance.AddScript(GetComponent<FluentScript>());
                over = true;
            }
        }

        void OnTriggerExit(Collider collider)
        {
            if (collider.attachedRigidbody.gameObject == PlayerGameObject)
            {
                FluentManager.Instance.RemoveScript(GetComponent<FluentScript>());
                over = false;
            }

        }
    }
}