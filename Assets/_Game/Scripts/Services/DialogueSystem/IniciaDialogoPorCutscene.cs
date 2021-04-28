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
    public bool comecaInicio;


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
            {
                over = true;
            }

            if (over && director.state == PlayState.Playing)
            {
                FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
                over = false;
            }
        }
        else
        {
            if (director.state == PlayState.Playing)
            {
                over = true;
            }

            if (over && director.state != PlayState.Playing)
            {
                FluentManager.Instance.ExecuteAction(GetComponent<FluentScript>());
                over = false;
            }
        }
    }

    void OnDisable()
    {
    }
}
