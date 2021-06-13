using System.Collections;
using System.Collections.Generic;
using Michsky.LSS;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public GameObject timelines;
    
    void OnEnable()
    {
        //playerAnim = playerAnimator.runtimeAnimatorController;
        //playerAnimator.runtimeAnimatorController = null;
    }

    void Update()
    {
        if (LoadingScreen.enableFading && timelines != null)
        {
            timelines.SetActive(true);
        }
    }
}
