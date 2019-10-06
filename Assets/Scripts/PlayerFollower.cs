using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float footstepVolume;
    [SerializeField] CinemachineVirtualCamera vCamObject;

    [SerializeField] float waitDistance;
    [SerializeField] float resumeDistance;
    [SerializeField] AudioClip[] lostSounds;

    [SerializeField]  float distanceFromPlayer;

    public bool doWalk = false;
    bool doPause = false;


    Queue<AudioClip> audioClips;

    AudioSource source;

    public AudioSource footstepSource;
    public AudioSource lostSource;

    GameObject player;


    bool isPlaying;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;

        audioClips = new Queue<AudioClip>();
        source = GetComponent<AudioSource>();

        vCamObject = GetComponent<CinemachineVirtualCamera>();

        vCamObject.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0f;
    }

    private void Update()
    {

        if (!doPause) { stopDistance(); }

        if (doWalk)
        {
            doMovement();
            footstepSource.volume = footstepVolume;

        }
        else
        {
            footstepSource.volume = 0f;
        }
        
    }

    private void stopDistance()
    {
        
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceFromPlayer > waitDistance)
        {
            doWalk = false;
            source.Pause();

            if (!isPlaying)
            {
                StartCoroutine(lostSound());
                isPlaying = true;
                footstepSource.volume = 0f;
            }

        } else if(distanceFromPlayer < resumeDistance)
        {
            if (isPlaying)
            {
                StopCoroutine(lostSound());
                source.Play();
            }

            doWalk = true;
            isPlaying = false;
        }
    }

    private IEnumerator lostSound()
    {
        
        lostSource.clip = lostSounds[UnityEngine.Random.Range(0, lostSounds.Length)];
        lostSource.Play();

        yield return new WaitForSeconds(UnityEngine.Random.Range(source.clip.length, source.clip.length+2));

        if(isPlaying)
        StartCoroutine(lostSound());
    }

    void doMovement()
    {
        vCamObject.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += walkSpeed * 0.001f;

        if (!footstepSource.isPlaying)
        {
            footstepSource.Play();
        }
        
    }

    public void playFromQueue()
    {
        source.clip = audioClips.Dequeue();
        source.Play();
    }

    public void addSound(AudioClip clip)
    {
        audioClips.Enqueue(clip);
    }



    public IEnumerator PauseWalk()
    {
        doPause = true;
        doWalk = false;

        yield return new WaitForSeconds(source.clip.length);

        doWalk = true;
        doPause = false;
    }
}
