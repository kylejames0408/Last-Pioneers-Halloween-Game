using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : CharacterScript
{
    public Rigidbody2D body;
    //publi Animator animation;

    Vector2 mov;

    //variable, getter and setter for the players currently held lantern
    public Lantern heldLantern;
    public Lantern HeldLantern
    {
        get { return heldLantern;}
        set { heldLantern = value;}
    }

    public bool touchingStand;

    // Start is called before the first frame update
    void Start()
    {
        heldLantern = null;
    }

    // Update is called once per frame
    void Update()
    {

        //This will actually move the player
        //float yInput = Input.GetAxis("Vertical");
        //float xInput = Input.GetAxis("Horizontal");
        //transform.Translate(xInput * 10f * Time.deltaTime, yInput * 10f * Time.deltaTime, 0f);

        //This is what locks the player on the screen. I'm not sure how needed this will be but might as well keep it for now.
        //Vector3 position = transform.position;
        //position.y = Mathf.Clamp(position.y, -5, 5);
        //position.x = Mathf.Clamp(position.x, -15, 15);
        //transform.position = position;

        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");

        //animator stuff go here
        //setFloat reminder
    }

    void FixedUpdate()
    {
  
        //Move the rigidbody2d based on the current position, axis, moveSpeed and deltaTime
        body.MovePosition(body.position + mov * moveSpeed * Time.fixedDeltaTime);

        // Flip the Character:
        Vector2 playerScale = transform.localScale;

        if (mov.x < 0)
        {
            playerScale.x = -1;
        }
        
        if (mov.x > 0)
        {
            playerScale.x = 1;
        }

        transform.localScale = playerScale;
    }



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

}
