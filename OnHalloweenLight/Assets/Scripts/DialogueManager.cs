using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentances;
    private Queue<string> sentancesSpokenTo;

    public Text nameText;
    public Text dialogueText;

    public bool spokenTo;
    public int publicChatCount;

    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();
        sentancesSpokenTo = new Queue<string>();
    }


    //takes in dialogue data and begins running through it
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentances.Clear();
        sentancesSpokenTo.Clear();

        spokenTo = dialogue.spokenTo;

        foreach (string sentance in dialogue.sentances)
        {
            sentances.Enqueue(sentance);
        }

        foreach (string sentanceSpoken in dialogue.sentancesSpokenTo)
        {
            sentancesSpokenTo.Enqueue(sentanceSpoken);
        }

        DisplayNextSentance();
    }

    //displays the next sentance in the sentance queue, or ends talking
    public void DisplayNextSentance()
    {
        Queue<string> dialogueChosen;


        if (spokenTo)
        {
            dialogueChosen = sentancesSpokenTo;
            //Debug.Log("You are using spoken to lines");
        } else
        {
            dialogueChosen = sentances;
            //Debug.Log("You are using not spoken to lines");
        }

        publicChatCount = dialogueChosen.Count;

        if (dialogueChosen.Count == 0)
        {
            GameManager.gameState = GameState.Game;
            nameText.text = "";
            dialogueText.text = "";
            LevelManager.textBox.GetComponent<SpriteRenderer>().enabled = false;
        
        }
        else
        {
            string sentance = dialogueChosen.Dequeue();

            if(nameText.text == "Perry the Pumpkin" && sentance == "pumpkin")
            {
                string stringstat = "still filled with critters!"; ;

                if(LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(0)) == "true")
                {
                    stringstat = "cleared of critters!";
                }
                sentance = "The " + LevelManager.questManager.GetQuestName(0) + " is " + stringstat;  
            }

            if (nameText.text == "Perry the Pumpkin" && sentance == "candy")
            {
                string stringstat = "looking pretty empty..."; ;

                if (LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(1)) == "true")
                {
                    stringstat = "overflowing with candy!";
                }
                sentance = "The " + LevelManager.questManager.GetQuestName(1) + " is " + stringstat;
            }

            if (nameText.text == "Perry the Pumpkin" && sentance == "midnight")
            {
                string stringstat = " happens at midnight, and when we're ready."; ;

                if (LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(2)) == "true")
                {
                    stringstat = "is ready to start Halloween Night!";
                }
                sentance = "The " + LevelManager.questManager.GetQuestName(2) + stringstat;
            }

            dialogueText.text = sentance;
        }
    }
}
