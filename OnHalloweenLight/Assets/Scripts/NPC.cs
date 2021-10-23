﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractObject, IInteractable
{
    public Dialogue dialogue;
    public bool inRange;


    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {

            case GameState.Game:
                    DoSomething();
                break;

            case GameState.Talking:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentance();
                }
                break;
                
        }
    }

    public override void DoSomething()
    {
        //checks if player is in range and pressing space
        if (Input.GetKeyDown(KeyCode.Space)&&inRange)
        {
            //show text box
            LevelManager.textBox.GetComponent<SpriteRenderer>().enabled = true;

            //switch game state and begin dialogue
            GameManager.gameState = GameState.Talking;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }



    //checks player collision
    // i did this like this rather than the range thing we have for the lanterns bc
    // its easier and makes things less complicated and our interacting code is pretty 
    // scuffed at this point, we probably should have made a "target" interactable
    // object in player so we are only ever interacting with 1 thing at once
    // but at this point itd be really annoying to do and were rescoping anyways
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            inRange = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            inRange = false;
        }
    }
}
