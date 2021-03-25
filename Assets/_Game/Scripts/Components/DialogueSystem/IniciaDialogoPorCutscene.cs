using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fluent;
using UnityEngine.Playables;

public class IniciaDialogoPorCutscene : MonoBehaviour
{
    bool everythingOk = false;
    bool over = false;

    public PlayableDirector director;


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

        if (director.state == PlayState.Playing)
        {
            over = true;
            //FluentManager.Instance.AddScript(GetComponent<FluentScript>());
        }

        if (over && director.state != PlayState.Playing)
        {
            FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
            over = false;
            //FluentManager.Instance.RemoveScript(GetComponent<FluentScript>());
        }
    }

    void OnDisable()
    {
    }
}
