using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : CharacterScript
{
    public Rigidbody2D body;
    public Animator animationRef;

    public List<string> spokenTo = new List<string>();

    Vector2 mov;

    //variable, getter and setter for the players currently held lantern
    public Lantern heldLantern;

    SpriteRenderer sprite;

    public Lantern HeldLantern
    {
        get { return heldLantern; }
        set { heldLantern = value; }
    }

    public bool touchingStand;
    public bool touchingNPC;
    public bool facingRight; //This tells the direction the player is facing.

    //If you were in range to pickup AND place on a stand then you would to both on the same frame. This fixes that
    public int frameCooldown;

    // Start is called before the first frame update
    void Start()
    {
        heldLantern = null;
        facingRight = true;
        frameCooldown = 0;

        //sets this player as the current one in level manager
        LevelManager.playerScript = this;
        animationRef.SetBool("noLantern", true);
        animationRef.SetBool("isPickUpDropOff", false);

        sprite = GetComponent<SpriteRenderer>();

        touchingStand = false;
        touchingNPC = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {
            case GameState.Menu:
                if (Input.GetKeyDown(KeyCode.Space))
                {

                }
                break;
            case GameState.Game:


                //This is for the player movement
                mov.x = Input.GetAxisRaw("Horizontal");
                mov.y = Input.GetAxisRaw("Vertical");

                if (frameCooldown > 0)
                {
                    frameCooldown--;
                }

                //animator stuff go here
                //setFloat reminder

                sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f) + 14;



                break;

            case GameState.Talking:

                mov = new Vector2(0, 0);

                break;

            case GameState.Pause:
                mov = new Vector2(0, 0);
                break;
        }


    }

    void FixedUpdate()
    {
  
        //Move the rigidbody2d based on the current position, axis, moveSpeed and deltaTime
        body.MovePosition(body.position + mov * moveSpeed * Time.fixedDeltaTime);

        // Flip the Character:
        Vector2 playerScale = transform.localScale;

        if(mov.x == 0 && mov.y==0)
        {
            animationRef.SetBool("isWalking", false);
            //Debug.Log("not walking");
        } else
        {
            animationRef.SetBool("isWalking", true);
            //Debug.Log("is walking");
        }

        //This is the player moving left
        if (mov.x < 0)
        {
            playerScale.x = -1;
            facingRight = false;
            //Debug.Log("Left");
        }
        
        //Moving right
        if (mov.x > 0)
        {
            playerScale.x = 1;
            facingRight = true;
            //Debug.Log("Right");
        }

        transform.localScale = playerScale;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Pumpkin")
    //    {
    //        other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(other.transform.position.y * -10f) + 14;
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag == "Pumpkin")
    //    {
    //        //if (other.transform.position.y < this.transform.position.y - 1.0f)
    //        //{
    //        //    Debug.Log("Foreground");
    //        //    other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
    //        //} else
    //        //{
    //        //    Debug.Log("Background");
    //        //    other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
    //        //}

    //        if (other.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 10.0f > sprite.sortingOrder)
    //        {
    //            Debug.Log("Foreground");
    //            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
    //        }
    //        else
    //        {
    //            Debug.Log("Background");
    //            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
    //        }
    //    }
    //}

    /*
    /// <summary>
    /// If the collision is with a lantern stand, set the touchingStand to true on the first hit.
    /// </summary>
    /// <param name="collision">Collision with the object touching it</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "LanternStand")
        {
            touchingStand = true;
        }
    }

    /// <summary>
    /// If the collision exited (not touching the lantern stand anymore), set the touchingStand 
    /// back to false since you are not in range to touch anymore.
    /// </summary>
    /// <param name="collision">Collision with the object leaving touch range</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "LanternStand")
        {
            touchingStand = !touchingStand;
        }
    }
    */
}
