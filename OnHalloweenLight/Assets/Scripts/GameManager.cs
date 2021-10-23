using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Menu, Game ,Pause,Talking};

public class GameManager : MonoBehaviour
{
    // Fields
    public static GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        // Set the gameState to be the menu
        gameState = GameState.Game;
    }

    // Update is called once per frame
    void Update()
    {
        // State Machine
        switch (gameState)
        {
            case GameState.Menu:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameState = GameState.Game; // start the game
                    SceneManager.LoadScene(sceneName: "2Room");
                }
                break;
            case GameState.Game:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Pause; // escape to menu (for now)
                   
                }
                break;
        }
    }

    void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);
    }
}
