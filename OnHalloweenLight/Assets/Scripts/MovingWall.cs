using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles disappearing wall functions.
/// </summary>
public class MovingWall : LanternTrigger
{
    /// <summary>
    /// Updates the wall.
    /// </summary>
    void Update()
    {
        // Triggers the wall
        doLanternTrigger();
    }

    /// <summary>
    /// Do something on lantner trigger.
    /// </summary>
    override public void doLanternTrigger()
    {
        // If the stands are full
        if (base.checkStands())
        {
            // Disable the wall
            Disable();

            // If it qualifies for the light houses quest
            if (questUpdate && LevelManager.questManager.GetQuestStatus("light houses") == "false")
            {
                // Check each NPC
                foreach (NPC npc in LevelManager.npcs)
                {
                    // If it's Perry
                    if (npc.name == "Perry the Pumpkin")
                    {
                        // Disable Perry
                        npc.enabled = false;
                    }
                }

                // Update the quest
                LevelManager.questManager.UpdateQuest("light houses", "true");
            }
        }
        else
        {
            // Enable the wall
            Enable();
        }
    }

    /// <summary>
    /// Enables the wall.
    /// </summary>
    void Enable()
    {
        // Enables collider and sprite
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    /// <summary>
    /// Disables the wall.
    /// </summary>
    void Disable()
    {
        // Disables collider and sprite
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}