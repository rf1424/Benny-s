using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLose : MonoBehaviour
{
    private void OnTriggerEnter(Collider p)
    {
        // Check if the object entering the trigger is the player
        if (p.CompareTag("Player"))
        {
            //EndGame();
            //Endtext.SetActive(true);
            if (GameData.wrongDoorChosen) {
                Debug.Log("You Lost");
            }
            
        }
    }
}
