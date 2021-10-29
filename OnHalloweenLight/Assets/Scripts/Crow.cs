using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Crow movement and interaction.
/// </summary>
public class Crow : InteractObject
{
    // Fields
    public static int crowCount = 0;
    private float offset;
    private int direction;

    /// <summary>
    /// Sets up the Crow object.
    /// </summary>
    void Start()
    {
        // Increment number of crows present
        crowCount++;

        // Initialize Fields
        offset = 0;

        // Generate random number, and if in the first half
        if (Random.Range(0.0f, 1.0f) < .5)
        {
            // Direct the crow negatively
            direction = -1;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else { direction = 1; } // Otherwise direct the crow positively
    }

    /// <summary>
    /// Updates the crows state in the game based on whether or not it touches lantern light.
    /// </summary>
    void Update()
    {
        // Check game state
        switch (GameManager.gameState)
        {
            // During game
            case GameState.Game:
                // If crow has not moved
                if(offset == 0)
                {
                    // If it touches lantern light
                    if(this.touchingLight)
                    {
                        // Set the crow to move, and remove from crow count
                        offset = 2;
                        crowCount--;
                    }
                }
                else
                {
                    // Move the crow off screen
                    this.transform.Translate(new Vector2(direction*(offset)/20,(offset*offset)/20));

                    // Checks crow position, if beyond 100y
                    if (this.transform.position.y > 100)
                    {
                        // Disable the crow
                        this.enabled = false;
                    }
                }
                break;
        }
    }
}
