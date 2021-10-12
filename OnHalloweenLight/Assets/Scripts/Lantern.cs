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

        GameManager.roomLanterns.Add(this);
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

            playerHandPos.x = GameManager.playerHand.transform.position.x;
            playerHandPos.y = GameManager.playerHand.transform.position.y - 0.7f;

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
        DoSomething();
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
            if(!isPlaced && !GameManager.player.touchingStand)
            {
                isPlaced = true;
                GameManager.player.heldLantern = null;
                print("latern placed");
                canGrab = true;
                this.GetComponent<BoxCollider2D>().enabled = true;

                Vector2 placePos = new Vector2(playerHandPos.x, playerHandPos.y - 0.30f);
                transform.position = placePos;
            } else
            {
                if(canGrab)
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    isPlaced = false;
                    canGrab = false;
                    GameManager.player.heldLantern = this;
                    print("latern picked up");
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
