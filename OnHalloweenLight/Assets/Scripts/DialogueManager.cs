using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class to manage dialogue.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    // Fields
    private Queue<string> sentances;
    private Queue<string> sentancesSpokenTo;

    public Text nameText;
    public Text dialogueText;
    public GameObject indicatorText;

    public bool spokenTo;
    public int publicChatCount;

    /// <summary>
    /// Initializes DialogueManager fields.
    /// </summary>
    void Start()
    {
        // Initialize Fields
        sentances = new Queue<string>();
        sentancesSpokenTo = new Queue<string>();
    }


    /// <summary>
    /// Runs through provided dialogue data.
    /// </summary>
    /// <param name="dialogue">The dialogue to run 
    public void StartDialogue(Dialogue dialogue)
    {
        // Disable the indicator text
        indicatorText.SetActive(false);

        // Set the NPC name text
        nameText.text = dialogue.name;

        // Clears sentences and sentences spoken
        sentances.Clear();
        sentancesSpokenTo.Clear();

        // Set whether or not the dialogue has been spoken before
        spokenTo = dialogue.spokenTo;

        // For each sentence in the provided dialogue
        foreach (string sentance in dialogue.sentances)
        {
            // Enqueue the sentence
            sentances.Enqueue(sentance);
        }

        // For each sentence spoken in the provided dialogue
        foreach (string sentanceSpoken in dialogue.sentancesSpokenTo)
        {
            // Enqueue the sentence
            sentancesSpokenTo.Enqueue(sentanceSpoken);
        }

        // Display the next sentence in the dialogue
        DisplayNextSentance();
    }

    /// <summary>
    /// Displays the next sentence in the sentence queue, or end the dialogue.
    /// </summary>
    public void DisplayNextSentance()
    {
        // Temporary Fields
        Queue<string> dialogueChosen;

        // If the Player has spoken this dialogue before
        if (spokenTo)
        {
            // Set the dialogue to previously spoken sentences
            dialogueChosen = sentancesSpokenTo;
        }
        else
        {
            // Set the dialogue to new sentences
            dialogueChosen = sentances;
        }

        // Gets the count of chosen dialogue
        publicChatCount = dialogueChosen.Count;

        // If there's no dialogue
        if (dialogueChosen.Count == 0)
        {
            // Return the game state to the game
            GameManager.gameState = GameState.Game;

            // Empty the text
            nameText.text = "";
            dialogueText.text = "";

            // Disable the text box
            LevelManager.textBox.GetComponent<SpriteRenderer>().enabled = false;

            // Re-enable the indicator
            indicatorText.SetActive(true);
        }
        else
        {
            // Dequeue the dialogue
            string sentance = dialogueChosen.Dequeue();

            #region SpecializedText
            // Specialized text for specific NPCs if they have more to say after certain conditions have been met.

            #region PerryThePumpkin
            // If talking to Perry and using sentence "pumpkin"
            if (nameText.text == "Perry the Pumpkin" && sentance == "pumpkin")
            {
                // Temporary Field
                string stringstat = "still filled with critters!"; ;

                // If pumpkin-patch quest is completed
                if (LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(0)) == "true")
                {
                    // Change the status
                    stringstat = "cleared of critters!";
                }

                // Set the sentence
                sentance = "The " + LevelManager.questManager.GetQuestName(0) + " is " + stringstat;  
            }

            // If talking to Perry and using sentence "candy"
            if (nameText.text == "Perry the Pumpkin" && sentance == "candy")
            {
                // Temporary Field
                string stringstat = "looking pretty dark..."; ;

                // If candy quest is completed
                if (LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(1)) == "true")
                {
                    // Change the status
                    stringstat = "brilliantly lit!";
                }

                // Set the sentence
                sentance = "The  houses are " + stringstat;
            }

            // If talking to Perry and using sentence "midnight"
            if (nameText.text == "Perry the Pumpkin" && sentance == "midnight")
            {
                // Temporary Field
                string stringstat = " happens when you've finished getting ready!"; ;

                // If quests are completed
                if (LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(2)) == "true")
                {
                    // Change the status
                    stringstat = "is ready to start Halloween Night!";
                }

                // Set the sentence
                sentance = "The " + LevelManager.questManager.GetQuestName(2) + stringstat;
            }
            #endregion

            #region SkittlesTheSkeleton
            // If talking to Skittles and the crows are cleared
            if (nameText.text == "Skittles the Skeleton" && Crow.crowCount == 0)
            {
                // Set the sentence
                sentance = "Thank you! Now that those pesky crows are gone I can focus on tending to my crops!";
            }
            #endregion
            #endregion

            // Set the sentence to display
            dialogueText.text = sentance;
        }
    }
}
