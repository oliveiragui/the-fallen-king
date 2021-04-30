using Fluent;
using UnityEngine;
using UnityEngine.Playables;

namespace _Game.Scripts.Services.DialogueSystem
{
    public class IniciaDialogoPorCutscene : MonoBehaviour
    {
        public PlayableDirector director;
        public bool comecaInicio;
        bool everythingOk;
        bool over;

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

            if (comecaInicio)
            {
                if (director.state != PlayState.Playing)
                    over = true;
                //FluentManager.Instance.AddScript(GetComponent<FluentScript>());

                if (over && director.state == PlayState.Playing)
                {
                    FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
                    over = false;
                    //FluentManager.Instance.RemoveScript(GetComponent<FluentScript>());
                }
            }
            else
            {
                if (director.state == PlayState.Playing)
                    over = true;
                //FluentManager.Instance.AddScript(GetComponent<FluentScript>());

                if (over && director.state != PlayState.Playing)
                {
                    FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
                    over = false;
                    //FluentManager.Instance.RemoveScript(GetComponent<FluentScript>());
                }
            }
        }

        void OnDisable() { }
    }
}