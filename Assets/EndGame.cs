using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject Endtext;
    private void OnTriggerEnter(Collider p)
    {
        // Check if the object entering the trigger is the player
        if (p.CompareTag("Player"))
        {
            //EndGame();
            Endtext.SetActive(true);
            Debug.Log("Game Ended");
        }
    }
}
