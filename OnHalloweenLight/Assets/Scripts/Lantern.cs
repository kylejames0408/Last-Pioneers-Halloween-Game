using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : InteractObject, IInteractable
{

    public GameObject playerHand;
    GameObject player;

    Vector2 playerHandPos;

    float lanternSpeed;
    bool isPlaced = false;
    bool canGrab = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHandPos = new Vector2(playerHand.transform.position.x, playerHand.transform.position.y - 0.7f);

        transform.position = playerHandPos;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //smooth transition looks bad
        //transform.position = Vector2.MoveTowards(transform.position, 
        //    playerHand.transform.position, 
        //    player.GetComponent<PlayerScript>().moveSpeed * Time.deltaTime);

        if(!isPlaced)
        {
            transform.position = playerHandPos;

            playerHandPos.x = playerHand.transform.position.x;
            playerHandPos.y = playerHand.transform.position.y - 0.7f;
        }
    }

    void Update()
    {
        DoSomething();
    }

    public override void DoSomething()
    {
        if (Input.GetKeyDown("space"))
        {
            if(!isPlaced)
            {
                isPlaced = true;
                print("latern placed");
                this.GetComponent<BoxCollider2D>().enabled = true;

                Vector2 placePos = new Vector2(playerHandPos.x, playerHandPos.y - 0.30f);
                transform.position = placePos;
            } else
            {
                if(canGrab)
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    isPlaced = false;
                    print("latern picked up");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") ;
        {
            canGrab = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canGrab = !canGrab;
    }
}
