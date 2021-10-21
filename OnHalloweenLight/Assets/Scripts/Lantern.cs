using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : InteractObject, IInteractable
{
    Vector2 playerHandPos;

    /// <summary>
    /// isPlaced - If it's on the ground at that moment or not.
    /// canGrab - If the player is in touching grabbable range. 
    /// </summary>
    public bool isPlaced = true;
    public bool canGrab = false;

    public GameObject player;

    SpriteRenderer sprite;

    /// <summary>
    /// Start by grabbing the player gameObject
    /// Set the handPos to the value
    /// 
    /// Disable the collider2D for the lantern so it doesn't push the player.
    /// </summary>
    void Start()
    {
        //saves references to both base player object and the script attached to it
       // playerScript = player.GetComponent<PlayerScript>();

        canGrab = false;

        this.GetComponent<BoxCollider2D>().enabled = true;
        base.isLantern = true;
        base.sprite = GetComponent<SpriteRenderer>();

        LevelManager.roomLanterns.Add(this);
        player.GetComponent<PlayerScript>().animationRef.SetBool("isPickUpDropOff", false);

        sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// FixedUpdate is called once per FPS frame which is locked at 60.
    /// </summary>
    void FixedUpdate()
    {
        //smooth transition looks bad
        //transform.position = Vector2.MoveTowards(transform.position, 
        //    playerHand.transform.position, 
        //    player.GetComponent<PlayerScript>().moveSpeed * Time.deltaTime);

        if(!isPlaced)
        {

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

        if (!isPlaced)
        {

            playerHandPos.x = LevelManager.playerHand.transform.position.x;
            playerHandPos.y = LevelManager.playerHand.transform.position.y - 0.7f;

            transform.position = playerHandPos;
        }

        DoSomething();

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
            //checks to make sure lantern is not placed, or the player is not close enough to a stand to place it on that
            if(!isPlaced && !LevelManager.playerScript.touchingStand && LevelManager.playerScript.frameCooldown == 0)
            {
                isPlaced = true;
                LevelManager.playerScript.heldLantern = null;
                print("latern placed");
                canGrab = true;
                this.GetComponent<BoxCollider2D>().enabled = true;

                Vector2 placePos = new Vector2(playerHandPos.x, playerHandPos.y - 0.30f);
                player.GetComponent<PlayerScript>().animationRef.SetBool("noLantern", false);
                player.GetComponent<PlayerScript>().animationRef.SetBool("droppingOff", true);
                player.GetComponent<PlayerScript>().animationRef.SetBool("pickingUp", false);
                transform.position = placePos;

                LevelManager.playerScript.frameCooldown = 5;
            } 
            else
            {
                if(canGrab && !LevelManager.playerScript.touchingStand && LevelManager.playerScript.heldLantern == null && LevelManager.playerScript.frameCooldown == 0)
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    isPlaced = false;
                    canGrab = false;
                    LevelManager.playerScript.heldLantern = this;
                    player.GetComponent<PlayerScript>().animationRef.SetBool("noLantern", false);
                    player.GetComponent<PlayerScript>().animationRef.SetBool("pickingUp", true);
                    player.GetComponent<PlayerScript>().animationRef.SetBool("droppingOff", false);
                    print("latern picked up");

                    LevelManager.playerScript.frameCooldown = 5;
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
            //this.GetComponent<BoxCollider2D>().enabled = false;
            //Experimenting with allowing a player to move through it. I'm willing to refer to other's expertise with this...
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
            //this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    /// <summary>
    /// If the sprite of the player (+ a pickup area) is touching the lantern
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns></returns>
    public bool PickUpCollision(GameObject player)
    {
        // get extents of player +/- 5
        float playerMinX = player.transform.position.x - player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMinX -= .25f;
        float playerMaxX = player.transform.position.x + player.GetComponent<BoxCollider2D>().size.x + player.GetComponent<BoxCollider2D>().offset.x;
        playerMaxX += .25f;
        float playerMinY = player.transform.position.y - player.GetComponent<BoxCollider2D>().size.y + player.GetComponent<BoxCollider2D>().offset.y;
        playerMinY -= .25f;
        float playerMaxY = player.transform.position.y + player.GetComponent<BoxCollider2D>().size.y + player.GetComponent<BoxCollider2D>().offset.y;
        playerMaxY += .25f;

        // get extents of lantern
        //Should technically account for offsets but whatever
        float lanternMinX = this.transform.position.x - this.GetComponent<BoxCollider2D>().size.x;
        float lanternMaxX = this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x;
        float lanternMinY = this.transform.position.y - this.GetComponent<BoxCollider2D>().size.y;
        float lanternMaxY = this.transform.position.y + this.GetComponent<BoxCollider2D>().size.y;

        // checks if one sprite is completely seperated from the other
        if (playerMaxX < lanternMinX) //player is completely to the left of lantern
        {
            //Debug.Log("playerMaxX < lanternMinX");
            return false;
        }
            
        if (lanternMaxX < playerMinX) //lantern is completely to the left of player
        {
            //Debug.Log("lanternMaxX < playerMinX");
            return false;
        }
        if (playerMaxY < lanternMinY) //player is completely below lantern
        {
            //Debug.Log("playerMaxY < lanternMinY");
            return false;
        }
        if (lanternMaxY < playerMinY) //lantern is completely below player
        {
            //Debug.Log("lanternMaxY < playerMinY");
            return false;
        }
        return true; // the only remaining alternative is that they are colliding
    }
}
