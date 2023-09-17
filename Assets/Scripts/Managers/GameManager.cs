using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DungeonGenerator2D))] 
public class GameManager : BaseGameManager
{
    // Start is called before the first frame update
    public void Start()
    {
        SetTargetState(Game.State.loaded);
    }

    public DungeonGenerator2D generator;

    public static GameManager instance { get; private set; }
    
    // set instance in this script's constructor
    public GameManager()
    {
        instance = this;
    }

    public override void UpdateTargetState()
    {
        Debug.Log("targetGameState=" + targetGameState);

        if (targetGameState == currentGameState)
            return;

        switch (targetGameState)
        {
            case Game.State.loaded:
                Loaded();
                break;

            case Game.State.gameStarting:
                GameStarting();
                StartGame();
                break;

            case Game.State.gameStarted:
                // fire the game started event
                GameStarted();
                SetTargetState(Game.State.gamePlaying);
                break;

            case Game.State.gamePlaying:
                break;

            case Game.State.gameEnding:
                GameEnding();
                EndGame();
                break;

            case Game.State.gameEnded:
                GameEnded();
                break;
        }
    }

    public override void Loaded()
    {
        base.Loaded();

        // load generator
        generator = GetComponent<DungeonGenerator2D>(); 
        // generator.MazeGenerator();
        generator.GenerateDungeon();
        SetTargetState(Game.State.gameStarting);
    }
    
    void StartGame()
    {
        // add a little delay to the start of the game, for people to prepare to run
        Invoke("StartRunning", 2f);
    }

    void StartRunning()
    {

        // start animation
        //_RunManCharacter.StartRunAnimation();
        //InvokeRepeating("AddScore", 0.5f, 0.5f);
        SetTargetState(Game.State.gameStarted);
    }

    void EndGame()
    {

        // schedule return to main menu in 4 seconds
        Invoke("ReturnToMenu", 4f);

        // stop the repeating invoke that awards score
        CancelInvoke("AddScore");

        // update target state
        SetTargetState(Game.State.gameEnded);
    }
}