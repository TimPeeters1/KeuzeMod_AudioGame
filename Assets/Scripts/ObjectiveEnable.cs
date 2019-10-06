using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveEnable : MonoBehaviour
{
    public GameObject Objective;

    private void Start()
    {
        Objective.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Objective.SetActive(true);
        }
    }
}

