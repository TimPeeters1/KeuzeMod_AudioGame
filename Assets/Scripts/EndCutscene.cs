using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class EndCutscene : MonoBehaviour
{
    [SerializeField] AudioSource outsideSound;
    [SerializeField] AudioSource insideSound;

    GameObject PlayerObject;

    PlayableDirector director;

    private void Start()
    {
        insideSound.volume = 0;
        outsideSound.volume = 1;

        director = FindObjectOfType<PlayableDirector>();
        director.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            PlayerObject = other.gameObject;


            PlayerObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
            PlayerObject.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
            PlayerObject.GetComponent<PlayerController>().enabled = false;
            PlayerObject.GetComponent<PlayerController>().footstepSource.volume = 0;

            director.enabled = true;
            director.Play();
        }
    }

    public void OpenDoor()
    {
        insideSound.volume = 0.3f;
        outsideSound.volume = 0;
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(1);
    }


}
