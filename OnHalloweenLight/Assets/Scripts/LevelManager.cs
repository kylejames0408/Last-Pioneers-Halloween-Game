using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //global list of lanterns
    public static List<Lantern> roomLanterns;
    public static List<LanternStand> roomStands;
    public static List<NPC> npcs;
    public static QuestManager questManager;


    //global variables for the player and their hand object
    public static PlayerScript playerScript;
    public static GameObject player;
    public static GameObject playerHand;

    public static GameObject textBox;
    public static GameObject pauseMenu;
    public static GameObject arrowDialogue;


    // Start is called before the first frame update
    void Start()
    {
        roomLanterns = new List<Lantern>();
        roomStands = new List<LanternStand>();
        npcs = new List<NPC>();



        //doing this with tagging was easier than doing it in the inspector
        player = GameObject.FindWithTag("Player");
        playerHand = GameObject.FindWithTag("Hand");

        textBox = GameObject.FindWithTag("TextBox");
        pauseMenu = GameObject.FindWithTag("PauseMenu");

        arrowDialogue = GameObject.FindWithTag("DialogIndicator");

        arrowDialogue.SetActive(false);

        pauseMenu.SetActive(false);

        questManager = this.GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {

        playerScript.touchingStand = false;

        bool indicatorPlaced = false;

        //If the player is touching an NPC nothing else will run
        if(!playerScript.touchingNPC)
        {

            for (int i = 0; i < roomStands.Count; i++)
            {

                roomStands[i].canGrab = roomStands[i].PickUpCollision(LevelManager.player);
                if (roomStands[i].canGrab)
                {
                    if (playerScript.heldLantern != null && roomStands[i].currentLantern == null)
                    {
                        roomStands[i].PickupIndicator();
                        indicatorPlaced = true;
                    }
                    playerScript.touchingStand = true;

                }
            }

            if(!indicatorPlaced)
            {
                for (int i = 0; i < roomLanterns.Count; i++)
                {
                    if (roomLanterns[i].canGrab && playerScript.heldLantern == null)
                    {
                        roomLanterns[i].PickupIndicator();
                        indicatorPlaced = true;
                    }
                }   
            }

            if(!indicatorPlaced)
            {
                roomStands[0].RemoveIndicator();
            }
        }

    }
}
