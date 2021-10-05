using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : InteractObject, IInteractable
{

    public GameObject playerHand;
    GameObject player;

    Vector2 playerHandPos;

    float lanternSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHandPos = new Vector2(playerHand.transform.position.x, playerHand.transform.position.y - 0.7f);

        transform.position = playerHandPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //smooth transition looks bad
        //transform.position = Vector2.MoveTowards(transform.position, 
        //    playerHand.transform.position, 
        //    player.GetComponent<PlayerScript>().moveSpeed * Time.deltaTime);
        transform.position = playerHandPos;

        playerHandPos.x = playerHand.transform.position.x;
        playerHandPos.y = playerHand.transform.position.y - 0.7f;
    }

    public override void DoSomething()
    {

    }
}
