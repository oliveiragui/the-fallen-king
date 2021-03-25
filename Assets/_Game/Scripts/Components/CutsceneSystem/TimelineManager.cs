using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private bool fix = false;
    public GameObject playerPivot;
    public RuntimeAnimatorController playerAnim;
    public GameObject player;
    public PlayableDirector director;

    void OnEnable()
    {
        //playerAnim = playerAnimator.runtimeAnimatorController;
        //playerAnimator.runtimeAnimatorController = null;
    }

    void Update()
    {
        if (director.state != PlayState.Playing)
        {
            //player.SetActive(true);
            //player.transform.SetPositionAndRotation(playerPivot.transform.position, playerPivot.transform.rotation);
            //fix = true;
        }
    }
}
