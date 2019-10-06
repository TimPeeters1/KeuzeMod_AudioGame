using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Valve.VR;

public class EndObjective : MonoBehaviour
{
    PlayableDirector director;


    public SteamVR_Action_Boolean isPressing = null;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            if (isPressing.state || Input.GetButtonDown("Fire1"))
            {
                director.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FollowPerson")
        {

        }
    }
}
