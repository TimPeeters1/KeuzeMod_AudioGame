using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAudio : MonoBehaviour
{
    Queue<AudioClip> audioQueue;

    AudioSource source;

    private void Start()
    {
        audioQueue = new Queue<AudioClip>();
        source = GetComponentInChildren<AudioSource>();
    }

    public void playFromQueue()
    {
        if (!source.isPlaying && audioQueue.Count > 0)
        {
            source.clip = audioQueue.Dequeue();
            source.Play();
        }
        else
        {
            playFromQueue();
            Debug.Log("Play next");
        }

    }

    public void addToQueue(AudioClip clip)
    {
        audioQueue.Enqueue(clip);
    }
}
