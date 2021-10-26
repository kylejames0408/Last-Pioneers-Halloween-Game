using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractObject, IInteractable
{
    public Dialogue dialogue;
    public bool inRange;
    public bool lightTouching = false;
    public bool spokenToAlready = false;


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
                SpriteUpdate();
                DoSomething();
                break;

            case GameState.Talking:
                if (Input.GetKeyDown(KeyCode.Space)&&inRange)
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentance();
                }
                break;

        }
    }

    public override void DoSomething()
    {
        //checks if player is in range and pressing space
        if (Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            CheckSpokenTo();

            //show text box
            LevelManager.textBox.GetComponent<SpriteRenderer>().enabled = true;

            //switch game state and begin dialogue
            GameManager.gameState = GameState.Talking;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

            if (!spokenToAlready)
            {
                spokenToAlready = true;
                LevelManager.playerScript.spokenTo.Add(this.name);
                dialogue.spokenTo = spokenToAlready;
                Debug.Log("Added name to spoken list");
            }
        }
    }

    public override void SpriteUpdate()
    {
        if (!touchingLight)
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteNoLight;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteWithLight;
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

            LevelManager.arrowDialogue.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y - 0.30f + this.GetComponent<SpriteRenderer>().bounds.size.y / 2,
            this.transform.position.z);
            LevelManager.arrowDialogue.SetActive(true);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            inRange = false;
            LevelManager.arrowDialogue.SetActive(false);
        }
    }

    private void CheckSpokenTo()
    {
        for (int i = 0; i < LevelManager.playerScript.spokenTo.Count; i++)
        {
            if (LevelManager.playerScript.spokenTo[i] == this.name)
            {
                Debug.Log("You've spoken to this NPC before!");
            }
        }
    }
}
