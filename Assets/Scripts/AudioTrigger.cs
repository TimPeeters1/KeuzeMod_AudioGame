using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTrigger : MonoBehaviour
{
    enum triggerType { player, personToFollow, other};
    [SerializeField] triggerType typeOfTrigger;
    bool hasPlayed;

    [SerializeField] bool stopWalk;

    [Space]
    [SerializeField] AudioClip clipToPlay;
    PlayerFollower followPerson;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        followPerson = FindObjectOfType<PlayerFollower>();

        //GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(typeOfTrigger == triggerType.player)
        {
            if(other.tag == "Player" && !hasPlayed)
            {
                other.GetComponent<PlayerAudio>().addToQueue(clipToPlay);
                other.GetComponent<PlayerAudio>().playFromQueue();

                hasPlayed = true;
            }
        }
        if (typeOfTrigger == triggerType.personToFollow)
        {
            if (other.tag == "FollowPerson" && !hasPlayed)
            {
                followPerson.addSound(clipToPlay);
                followPerson.playFromQueue();

                if (stopWalk)
                {
                    followPerson.StartCoroutine(followPerson.PauseWalk());
                }

                hasPlayed = true;
            }
        }
        if (typeOfTrigger == triggerType.other)
        {
            if (other.tag == "Player" && !hasPlayed)
            {
                source.clip = clipToPlay;
                source.Play();

                hasPlayed = true;

                if (stopWalk)
                {
                    followPerson.StartCoroutine(followPerson.PauseWalk());
                }
            }
        }
    }
}
