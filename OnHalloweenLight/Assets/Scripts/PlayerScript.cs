using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : CharacterScript
{
    [SerializeField]
    public float moveSpeed = 2.0f;

    public Rigidbody2D body;
    //publi Animator animation;

    Vector2 mov;

    // Start is called before the first frame update
    void Start()
    {

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
}
