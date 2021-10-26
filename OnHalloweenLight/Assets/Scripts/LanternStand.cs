using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternStand : InteractObject, IInteractable
{
    //references to player script, and lantern on stand
    public Lantern currentLantern;

    /// <summary>
    /// hasLantern - If a lantern is present on the stand
    /// canGrab - If the player is in touching grabbable range. 
    /// player - The player.
    /// </summary>
    public bool hasLantern = false;
    public bool canGrab = false;

    private Text indicatorText;

    /// <summary>
    /// Set the handPos to the value
    /// 
    /// Disable the collider2D for the lantern so it doesn't push the player.
    /// </summary>
    void Start()
    {
        //initalize stand
        currentLantern = null;

        base.sprite = GetComponent<SpriteRenderer>();
        LevelManager.roomStands.Add(this);

        indicatorText = GameObject.Find("IndicatorCanvas/IndicatorText").GetComponent<Text>();
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
        //base.InLanternRange();
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
            //Debug.Log(canGrab);

            if (!hasLantern)
            {
                //checks if the player is holding a lantern, is close enough, and there is no lantern on the stand
                if(LevelManager.playerScript.heldLantern !=null && canGrab && LevelManager.playerScript.frameCooldown == 0)
                {
                    //set the current lantern to the players one and remove the lantern from the player
                    currentLantern = LevelManager.playerScript.heldLantern;
                    currentLantern.isPlaced = true;
                    LevelManager.playerScript.heldLantern = null;
                    hasLantern = true;


                    //move the position to the top of the stand
                    Vector2 placePos = new Vector2(this.transform.position.x, this.transform.position.y + 1f);
                    currentLantern.transform.position = placePos;
                    currentLantern.onStand = true;

                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("noLantern", false);
                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("droppingOff", true);
                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("pickingUp", false);
                    //print("lantern placed on stand");
                    indicatorText.text = "";

                    LevelManager.playerScript.frameCooldown = 5;
                }
            }
            else
            {
                //checks if the player is close enough to the stand and they have nothing in their hand
                if (canGrab && LevelManager.playerScript.heldLantern == null && LevelManager.playerScript.frameCooldown == 0)
                {
                    //removes lantern from stand and puts it in player hand
                    LevelManager.playerScript.heldLantern = currentLantern;
                    currentLantern.isPlaced = false;
                    currentLantern.onStand = false;
                    currentLantern = null;
                    hasLantern = false;

                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("noLantern", false);
                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("pickingUp", true);
                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("droppingOff", false);

                    print("latern picked up from stand");
                    indicatorText.text = "Press SPACE to drop Lantern";

                    LevelManager.playerScript.frameCooldown = 5;
                }
            }
        }
    }

    /// <summary>
    /// If the sprite of the player (+ a pickup area) is touching the lantern stand
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns></returns>
    public bool PickUpCollision(GameObject player)
    {
        // get extents of player +/- some number that I change all the time
        float playerMinX = player.transform.position.x - player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMinX -= .5f;
        float playerMaxX = player.transform.position.x + player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMaxX += .5f;
        float playerMinY = player.transform.position.y - player.GetComponent<BoxCollider2D>().size.y + player.GetComponent<BoxCollider2D>().offset.y;
        playerMinY -= .5f;
        float playerMaxY = player.transform.position.y + player.GetComponent<BoxCollider2D>().size.y + player.GetComponent<BoxCollider2D>().offset.y;
        playerMaxY += .5f;

        // get horizontal extents of lantern
        //Should technically account for offsets but whatever
        float standMinX = this.transform.position.x - this.GetComponent<BoxCollider2D>().size.x;
        float standMaxX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x;
        float standMinY = this.transform.position.y - this.GetComponent<BoxCollider2D>().size.y;
        float standMaxY = this.transform.position.y + this.GetComponent<BoxCollider2D>().size.y;

        // checks if one sprite is completely seperated from the other
        if (playerMaxX < standMinX) //player is completely to the left of stand
        {
            //Debug.Log("player too far to the left");
            return false;
        }

        if (standMaxX < playerMinX) //stand is completely to the left of player
        {
            //Debug.Log("player too far to the right");
            return false;
        }
        if (playerMaxY < standMinY) //player is completely below stand
        {
            //Debug.Log("player too far down");
            return false;
        }
        if (standMaxY < playerMinY) //stand is completely below player
        {
            //Debug.Log(standMaxY);
            //Debug.Log(playerMinY);
            return false;
        }
        //Debug.Log("Is in range to grab");

        //At this point the player is in range to grab, we want to check if he's facing the right direction tho
        //These two variables are reset to be JUST the player
        playerMinX = player.transform.position.x - player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMinX += .2f; //This just makes it a little cleaner
        playerMaxX = player.transform.position.x + player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        if (playerMaxX < standMinX) //player is to the left of the stand
        {
            //If they are to the left and facint right
            if(LevelManager.playerScript.facingRight)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        if (standMaxX < playerMinX) //player is to the right of the stand
        {
            //If they are to the right and facing left
            if (!LevelManager.playerScript.facingRight)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //If the player is under or over the stand then it can pick up
        return true; // the only remaining alternative is that they are colliding
    }
}

