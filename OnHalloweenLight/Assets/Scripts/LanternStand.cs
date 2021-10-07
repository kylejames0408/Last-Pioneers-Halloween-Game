using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LanternStand : InteractObject, IInteractable
{
    //references to player script, and lantern on stand
    PlayerScript playerScript;
    Lantern currentLantern;



    /// <summary>
    /// hasLantern - If a lantern is present on the stand
    /// canGrab - If the player is in touching grabbable range. 
    /// player - The player.
    /// </summary>
    bool hasLantern = false;
    bool canGrab = false;

    /// <summary>
    /// Set the handPos to the value
    /// 
    /// Disable the collider2D for the lantern so it doesn't push the player.
    /// </summary>
    void Start()
    {
        //initalize stand
        playerScript = player.GetComponent<PlayerScript>();
        currentLantern = null;

        base.sprite = GetComponent<SpriteRenderer>();
    }

   

    /// <summary>
    /// Update is called once per frame based on your computer's
    /// FPS rendering ability (could be more or less or equal to 60)
    /// 
    /// Constantly check for collision and what to do
    /// </summary>
    void Update()
    {
        DoSomething();
        base.InLanternRange();

    }

    /// <summary>
    /// If the space bar is pressed and there is a lantern on the stand, pick it up
    /// 
    /// If a lantern is in the players hand and space is pressed, put it on the stand
    /// </summary>
    public override void DoSomething()
    {
        if (Input.GetKeyDown("space"))
        {
            if (!hasLantern)
            {
                //checks if the player is holding a lantern, is close enough, and there is no lantern on the stand
                if(playerScript.heldLantern !=null && canGrab)
                {
                    //set the current lantern to the players one and remove the lantern from the player
                    currentLantern = playerScript.heldLantern;
                    currentLantern.isPlaced = true;
                    playerScript.heldLantern = null;
                    hasLantern = true;


                    //move the position to the top of the stand
                    Vector2 placePos = new Vector2(this.transform.position.x, this.transform.position.y + 0.40f);
                    currentLantern.transform.position = placePos;
                    print("lantern placed on stand");
                }
            }
            else
            {
                //checks if the player is close enough to the stand and they have nothing in their hand
                if (canGrab && playerScript.heldLantern == null)
                {
                    //removes lantern from stand and puts it in player hand
                    playerScript.heldLantern = currentLantern;
                    currentLantern.isPlaced = false;
                    currentLantern = null;
                    hasLantern = false;

                    print("latern picked up from stand");
                }
            }
        }
    }

    /// <summary>
    /// If the collision is with the player, set the canGrab to true on the first hit.
    /// </summary>
    /// <param name="collision">Collision with the object touching it</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            canGrab = true;
        }
    }

    /// <summary>
    /// If the collision exited (not touching the player anymore), set the canGrab 
    /// back to false since you are not in range to touch anymore.
    /// </summary>
    /// <param name="collision">Collision with the object leaving touch range</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            canGrab = !canGrab;
        }
    }
}

