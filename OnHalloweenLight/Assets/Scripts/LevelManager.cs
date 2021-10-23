using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //global list of lanterns
    public static List<Lantern> roomLanterns;
    public static List<LanternStand> roomStands;


    //global variables for the player and their hand object
    public static PlayerScript playerScript;
    public static GameObject player;
    public static GameObject playerHand;

    public static GameObject textBox;



    // Start is called before the first frame update
    void Start()
    {
        roomLanterns = new List<Lantern>();
        roomStands = new List<LanternStand>();


        //doing this with tagging was easier than doing it in the inspector
        player = GameObject.FindWithTag("Player");
        playerHand = GameObject.FindWithTag("Hand");

        textBox = GameObject.FindWithTag("TextBox");
    }

    // Update is called once per frame
    void Update()
    {
        playerScript.touchingStand = false;

        for(int i = 0; i < roomStands.Count; i++)
        {
            roomStands[i].canGrab = roomStands[i].PickUpCollision(LevelManager.player);
            if(roomStands[i].canGrab)
            {
                playerScript.touchingStand = true;
            }
        }
    }
}
