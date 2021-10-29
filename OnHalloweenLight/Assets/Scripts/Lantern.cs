using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Lantern class.
/// </summary>
public class Lantern : InteractObject, IInteractable
{
    // Fields
    Vector2 playerHandPos;
    List<GameObject> touchingList;
    public bool isPlaced = true; // if lantern is on the ground
    public bool canGrab = false; // if the player is in grabbable range
    public bool onStand = false;
    private Text indicatorText;

    /// <summary>
    /// Initialize fields and update the level manager.
    /// </summary>
    void Start()
    {
        // Initialize fields
        canGrab = false;
        touchingList = new List<GameObject>();

        this.GetComponent<BoxCollider2D>().enabled = false;
        base.isLantern = true;
        base.sprite = GetComponent<SpriteRenderer>();

        sprite = GetComponent<SpriteRenderer>();

        indicatorText = GameObject.Find("IndicatorCanvas/IndicatorText").GetComponent<Text>();

        // Update LevelManager
        LevelManager.roomLanterns.Add(this);
        LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("isPickUpDropOff", false);
    }

    /// <summary>
    /// Update the lantern's position; called once per FPS frame which is locked at 60.
    /// </summary>
    void FixedUpdate()
    {
        //If the lantern is not placed
        if (!isPlaced)
        {
            // Follow the player's hand
            playerHandPos.x = LevelManager.playerHand.transform.position.x;
            playerHandPos.y = LevelManager.playerHand.transform.position.y - 0.7f;

            transform.position = playerHandPos;
        }
    }

    /// <summary>
    /// Update is called once per frame based on your computer's
    /// FPS rendering ability (could be more or less or equal to 60)
    /// 
    /// Constantly check for collision and what to do
    /// </summary>
    void Update()
    {
        canGrab = PickUpCollision(LevelManager.player);

        //If the lantern is not placed it follows the player
        if (!isPlaced)
        {

            playerHandPos.x = LevelManager.playerHand.transform.position.x;
            playerHandPos.y = LevelManager.playerHand.transform.position.y - 0.7f;

            transform.position = playerHandPos;
        }

        DoSomething();
        LightTouching();

        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
    }

    /// <summary>
    /// If the space bar is pressed and the lantern isn't placed yet, place the lantern
    /// y position - 0.30f on the ground.
    /// 
    /// If the space bar is pressed, and is in collision (can be picked up) range, then 
    /// pick up the lantern and have it follow the player movement again.
    /// </summary>
    public override void DoSomething()
    {
        if (Input.GetKeyDown("space"))
        {
            //checks to make sure lantern is not placed, or the player is not close enough to a stand to place it on that, and the player has not acted recently, and they are not touching an NPC
            if (!isPlaced && !LevelManager.playerScript.touchingStand && LevelManager.playerScript.frameCooldown == 0 && !LevelManager.playerScript.touchingNPC)
            {
                isPlaced = true;
                LevelManager.playerScript.heldLantern = null;
                //print("latern placed");
                canGrab = true;
                this.GetComponent<BoxCollider2D>().enabled = false;

                Vector2 placePos = new Vector2(playerHandPos.x, playerHandPos.y - 0.30f);
                LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("noLantern", false);
                LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("droppingOff", true);
                LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("pickingUp", false);
                transform.position = placePos;

                LevelManager.playerScript.frameCooldown = 5;
                indicatorText.text = "";
            }
            else
            {
                //If the lantern is not on a stand and they are in range of the player and the player has not acted recently, and they are not touching an NPC
                if (!onStand && canGrab &&  LevelManager.playerScript.heldLantern == null && LevelManager.playerScript.frameCooldown == 0 && !LevelManager.playerScript.touchingNPC)
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    isPlaced = false;
                    canGrab = false;
                    LevelManager.playerScript.heldLantern = this;
                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("noLantern", false);
                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("pickingUp", true);
                    LevelManager.player.GetComponent<PlayerScript>().animationRef.SetBool("droppingOff", false);
                    //print("latern picked up");

                    LevelManager.playerScript.frameCooldown = 5;
                    indicatorText.text = "Press SPACE to drop Lantern";
                }
            }
        }
    }

    /// <summary>
    /// If the sprite of the player (+ a pickup area) is touching the lantern
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns></returns>
    public bool PickUpCollision(GameObject player)
    {
        // get extents of player +/- some number that I change all the time
        float playerMinX = player.transform.position.x - player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMinX -= .1f;
        float playerMaxX = player.transform.position.x + player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMaxX += .1f;
        float playerMinY = player.transform.position.y - player.GetComponent<BoxCollider2D>().size.y + player.GetComponent<BoxCollider2D>().offset.y;
        playerMinY -= .1f;
        float playerMaxY = player.transform.position.y + player.GetComponent<BoxCollider2D>().size.y + player.GetComponent<BoxCollider2D>().offset.y;
        playerMaxY += .1f;

        // get horizontal extents of lantern
        //Should technically account for offsets but whatever
        float lanternMinX = this.transform.position.x - this.GetComponent<BoxCollider2D>().size.x;
        float lanternMaxX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x;
        float lanternMinY = this.transform.position.y - this.GetComponent<BoxCollider2D>().size.y;
        float lanternMaxY = this.transform.position.y + this.GetComponent<BoxCollider2D>().size.y;

        // checks if one sprite is completely seperated from the other
        if (playerMaxX < lanternMinX) //player is completely to the left of lantern
        {
            //Debug.Log("player too far to the left");
            return false;
        }

        if (lanternMaxX < playerMinX) //lantern is completely to the left of player
        {
            //Debug.Log("player too far to the right");
            return false;
        }
        if (playerMaxY < lanternMinY) //player is completely below lantern
        {
            //Debug.Log("player too far down");
            return false;
        }
        if (lanternMaxY < playerMinY) //lantern is completely below player
        {
            //Debug.Log("player too far up");
            return false;
        }

        //At this point the player is in range to grab, we want to check if he's facing the right direction tho
        //These two variables are reset to be JUST the player
        playerMinX = player.transform.position.x - player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMinX += .4f; //This just makes it a little cleaner
        playerMaxX = player.transform.position.x + player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        if (playerMaxX < lanternMinX) //player is to the left of the lantern
        {
            //If they are to the left and facint right
            if (LevelManager.playerScript.facingRight)
            {
                //Debug.Log("Is in range to grab");
                return true;
            }
            else
            {
                //Debug.Log("Facing wrong way");
                return false;
            }

        }
        if (lanternMaxX < playerMinX) //player is to the right of the lantern
        {
            //If they are to the right and facing left
            if (!LevelManager.playerScript.facingRight)
            {
                //Debug.Log("Is in range to grab");
                return true;
            }
            else
            {
                //Debug.Log("Facing wrong way");
                return false;
            }

        }

        //If the player is under or over the lantern then it can pick up
        //Debug.Log("Is in range to grab");
        return true; // the only remaining alternative is that they are colliding
    }

    /// <summary>
    /// This is a method for it something is in range of the lantern
    /// </summary>
    public void LightTouching()
    {
        GameObject[] touching = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject l in touching)
        {
            float dist = Vector3.Distance(l.transform.position, this.transform.position);
            //Debug.Log(dist);

            if (dist <= 3.8f)
            {
                
                if (!touchingList.Contains(l))
                {
                    touchingList.Add(l);
                    Debug.Log("Added object to touching list");
                }
                    
            }
            else if (dist > 3.8f)
            {
                //Debug.Log("Out of range");
                if (touchingList.Contains(l))
                {
                    Debug.Log("TOUCHING: " + touchingList.Count);
                    int index = touchingList.IndexOf(l);

                    touchingList.Remove(l);

                    if (touchingList.Count == 0)
                    {
                        l.GetComponent<InteractObject>().touchingLight = false;
                       // Debug.Log("UPDATE STAT: " + l.GetComponent<InteractObject>().touchingLight);
                    }
                }
                    
            }
        }

        for (int i = 0; i < touchingList.Count; i++)
        {
            touchingList[i].GetComponent<InteractObject>().touchingLight = true;
           // Debug.Log("UPDATE STAT: " + touching[i].GetComponent<InteractObject>().touchingLight);
        }
    }

    /// <summary>
    /// Places the arrow indicator
    /// </summary>
    public void PickupIndicator()
    {
        LevelManager.arrowDialogue.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y - 0.30f + this.GetComponent<SpriteRenderer>().bounds.size.y / 2,
            this.transform.position.z);
        LevelManager.arrowDialogue.SetActive(true);

        indicatorText.text = "Press SPACE to pick up Lantern";
    }

}
