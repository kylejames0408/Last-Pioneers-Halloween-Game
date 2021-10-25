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
            Debug.Log("You are using spoken to lines");
        } else
        {
            dialogueChosen = sentances;
            Debug.Log("You are using not spoken to lines");
        }

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
            dialogueText.text = sentance;
        }
    }
}
