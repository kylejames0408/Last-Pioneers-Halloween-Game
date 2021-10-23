using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentances;

    public Text nameText;
    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();
    }


    //takes in dialogue data and begins running through it
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentances.Clear();

        foreach (string sentance in dialogue.sentances)
        {
            sentances.Enqueue(sentance);
        }

        DisplayNextSentance();
    }

    //displays the next sentance in the sentance queue, or ends talking
    public void DisplayNextSentance()
    {
        if (sentances.Count == 0)
        {
            GameManager.gameState = GameState.Game;
            nameText.text = "";
            dialogueText.text = "";
            LevelManager.textBox.GetComponent<SpriteRenderer>().enabled = false;
        
        }
        else
        {
            string sentance = sentances.Dequeue();
            dialogueText.text = sentance;
        }
    }
}
