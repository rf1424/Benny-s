using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public List<GameObject> doors;
    // Start is called before the first frame update

    

    void Start()
    {
        int[] chosenDoors = { 0, 2, 4 }; // temp

        // see which doors were chosen
        for (int i = 0; i < 3; i++) {

            // see which doors for chosen (iterate 3 times), temp
            int chosenDoorNum = chosenDoors[i];

            // if door i was chosen, open
            if (chosenDoorNum != -1 && chosenDoorNum < doors.Count) {
                openDoor(doors[chosenDoorNum]);

                Renderer renderer = doors[chosenDoorNum].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.blue; // Change to chosen color
                }
            }
        }
                

            //// door contains benjamin
            //if (i == 3) {
            //    // LOSE
            //    Debug.Log("You chose the wrong door! Game Over!");
            //}
        }
        
    

    void openDoor(GameObject door)
    {
        // animate? 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
