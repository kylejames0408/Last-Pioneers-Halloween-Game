using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Enumeration for various game states.
/// </summary>
public enum GameState { Menu, Game, Pause, Talking };

/// <summary>
/// Manages various game tasks.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Fields
    [SerializeField] public static GameState gameState;

    /// <summary>
    /// Set the initial game state.
    /// </summary>
    void Start()
    {
        // Set the gameState to be the menu
        gameState = GameState.Game;
    }

    /// <summary>
    /// Perform various operations dependent on game state.
    /// </summary>
    void Update()
    {
        // State Machine
        switch (gameState)
        {
            case GameState.Menu:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Start the game
                    gameState = GameState.Game;
                    SceneManager.LoadScene(sceneName: "2Room");
                }
                break;
            case GameState.Game:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    // Pause the game
                    LevelManager.pauseMenu.SetActive(true);
                    gameState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    // Unpause the game
                    LevelManager.pauseMenu.SetActive(false);
                    gameState = GameState.Game;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    // Return to the menu
                    SceneManager.LoadScene(sceneName: "Menu");
                }
                break;
        }
    }

    /// <summary>
    /// Keeps the game manager persistant.
    /// </summary>
    void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);
    }
}
