using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //global list of lanterns
    public static List<Lantern> roomLanterns;

    //global variables for the player and their hand object
    public static PlayerScript player;
    public static GameObject playerHand;



    // Start is called before the first frame update
    void Start()
    {
        roomLanterns = new List<Lantern>();

        //doing this with tagging was easier than doing it in the inspector
        playerHand = GameObject.FindWithTag("Hand");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
