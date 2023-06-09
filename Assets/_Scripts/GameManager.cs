using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    //Fetch Compnents in the state
    Start,
    //Pass/Initialize required components in respective class to child classes
    Initialize,
    //Game is ready to play
    Playing,
    Paused,
    Resume
}
//Initialized on MainMenu scene
//Handles Game state of game, like Start, Initialize and ready states
//Fires event when state is changed so other classes can act accordingly
public class GameManager : SingletonBase<GameManager> 
{

    public static UnityAction<GameState> OnGameManagerStateChanged;
    private GameState m_CurrentState;
    public static GameState CurrentState => s_Instance.m_CurrentState;


    private void OnEnable()
    {
        LevelManager.OnLevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int arg0)
    {
        ChangeGameManagerState(GameState.Start);
    }

    void Start()
    {
        ChangeGameManagerState(GameState.Start);
    }

    public void ChangeGameManagerState(GameState newState)
    {
        m_CurrentState = newState;
        OnGameManagerStateChanged?.Invoke(newState);
        switch (newState)
        {
            case GameState.Start:
                StartStateHandler();
                break;
            case GameState.Initialize:
                InitializeStateHandler();
                break;
            case GameState.Playing:
                PlayingStateHandler();
                break;
            case GameState.Paused:
                PausedStateHandler();
                break;
            case GameState.Resume:
                ResumeStateHandler();
                break;
            default:
                break;

        }
    }

    private void StartStateHandler()
    {
        ChangeGameManagerState(GameState.Initialize);
    }

    private void InitializeStateHandler()
    {
        ChangeGameManagerState(GameState.Playing);
    }

    private void PlayingStateHandler()
    {

    }

    private void PausedStateHandler()
    {

    }

    private void ResumeStateHandler() 
    {

    }
}
