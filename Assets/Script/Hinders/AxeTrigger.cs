using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AxeTrigger : MonoBehaviour
{
    public GameObject[] ActiveObject;


    private void StartActive()
    {
        for (int i = 0; i < ActiveObject.Length; i++)
        {
            ActiveObject[i].transform.GetComponent<AxeMovement>().enabled = true;
            ActiveObject[i].transform.GetChild(0).transform.GetComponent<Animator>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Coin") StartActive();
    }
}