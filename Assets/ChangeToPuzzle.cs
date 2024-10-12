using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToPuzzle : MonoBehaviour
{
    public GameObject Controller;

    private void OnTriggerEnter(Collider p)
    {
        // Check if the object entering the trigger is the player
        if (p.CompareTag("Player"))
        {
            if (!GameData.puzzleSolved) {
                Cursor.lockState = CursorLockMode.None;
                GameData.puzzleSolved = true;

                // switch to Puzzle
                SceneManager.LoadScene("SlidingPuzzle");

                // hide controller
                Controller.SetActive(false);
            }
            
        }
    }
}
