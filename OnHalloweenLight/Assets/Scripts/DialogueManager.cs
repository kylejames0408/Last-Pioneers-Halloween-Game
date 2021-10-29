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
    public bool spokenTo;
    public int publicChatCount;

    private Queue<string> sentences;
    private Queue<string> sentencesSpokenTo;

    public Text nameText;
    public Text dialogueText;
    public GameObject indicatorText;

    /// <summary>
    /// Initializes DialogueManager fields.
    /// </summary>
    void Start()
    {
        // Initialize Fields
        sentences = new Queue<string>();
        sentencesSpokenTo = new Queue<string>();
    }

    /// <summary>
    /// Runs through provided dialogue data.
    /// </summary>
    /// <param name="dialogue">The dialogue to run through.</param>
    public void StartDialogue(Dialogue dialogue)
    {
        // Disable the indicator text
        indicatorText.SetActive(false);

        // Set the NPC name text
        nameText.text = dialogue.name;

        // Clears sentences and sentences spoken
        sentences.Clear();
        sentencesSpokenTo.Clear();

        // Set whether or not the dialogue has been spoken before
        spokenTo = dialogue.spokenTo;

        // For each sentence in the provided dialogue
        foreach (string sentence in dialogue.sentences)
        {
            // Enqueue the sentence
            sentences.Enqueue(sentence);
        }

        // For each sentence spoken in the provided dialogue
        foreach (string sentenceSpoken in dialogue.sentencesSpokenTo)
        {
            // Enqueue the sentence
            sentencesSpokenTo.Enqueue(sentenceSpoken);
        }

        // Display the next sentence in the dialogue
        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence in the sentence queue, or end the dialogue.
    /// </summary>
    public void DisplayNextSentence()
    {
        // Temporary Fields
        Queue<string> dialogueChosen;

        // If the Player has spoken this dialogue before
        if (spokenTo)
        {
            // Set the dialogue to previously spoken sentences
            dialogueChosen = sentencesSpokenTo;
        } 
        else
        {
            // Set the dialogue to new sentences
            dialogueChosen = sentences;
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
            string sentence = dialogueChosen.Dequeue();

            #region SpecializedText
            // Specialized text for specific NPCs if they have more to say after certain conditions have been met.

            #region PerryThePumpkin
            // If talking to Perry and using sentence "pumpkin"
            if (nameText.text == "Perry the Pumpkin" && sentence == "pumpkin")
            {
                // Temporary Field
                string stringstat = "still filled with critters!"; ;

                // If pumpkin-patch quest is completed
                if(LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(0)) == "true")
                {
                    // Change the status
                    stringstat = "cleared of critters!";
                }

                // Set the sentence
                sentence = "The " + LevelManager.questManager.GetQuestName(0) + " is " + stringstat;  
            }

            // If talking to Perry and using sentence "candy"
            if (nameText.text == "Perry the Pumpkin" && sentence == "candy")
            {
                // Temporary Field
                string stringstat = "looking pretty empty..."; ;

                // If candy quest is completed
                if (LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(1)) == "true")
                {
                    // Change the status
                    stringstat = "overflowing with candy!";
                }

                // Set the sentence
                sentence = "The " + LevelManager.questManager.GetQuestName(1) + " is " + stringstat;
            }

            // If talking to Perry and using sentence "midnight"
            if (nameText.text == "Perry the Pumpkin" && sentence == "midnight")
            {
                // Temporary Field
                string stringstat = " happens at midnight, and when we're ready."; ;

                // If quests are completed
                if (LevelManager.questManager.GetQuestStatus(LevelManager.questManager.GetQuestName(2)) == "true")
                {
                    // Change the status
                    stringstat = "is ready to start Halloween Night!";
                }
                
                // Set the sentence
                sentence = "The " + LevelManager.questManager.GetQuestName(2) + stringstat;
            }
            #endregion

            #region SkittlesTheSkeleton
            // If talking to Skittles and the crows are cleared
            if (nameText.text == "Skittles the Skeleton" && Crow.crowCount == 0)
            {
                // Set the sentence
                sentence = "Thank you! Now that those pesky crows are gone I can focus on tending to my crops!";
            }
            #endregion
            #endregion

            // Set the sentence to display
            dialogueText.text = sentence;
        }
    }
}
