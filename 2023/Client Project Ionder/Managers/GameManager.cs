using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    public GameState GetState
    {
        get { return gameState; }
    }

    public UI_InputManager UI_Manager;
    public AudioManager audioManager;
    public CustomizationManager customizationManager;
    public TimeManager timeManager;
    public EmailGeneratorTesting emailGen;

    private bool Bootup = false;

    public static event Action<GameState> onGameStateChanged;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //SwitchState(GameState.MainMenuState);
    }

    public void SwitchState(GameState newState)
    {
        GameState oldState = gameState;
        gameState = newState;

        switch (newState)
        {
            case GameState.MainMenuState:
                UI_Manager.MainMenuCanvas.gameObject.SetActive(true);
                timeManager.TV.gameObject.SetActive(false);
                break;

            case GameState.ActiveGameplayState:
                UI_Manager.ReportCanvas.gameObject.SetActive(false);
                UI_Manager.ActiveGameplayCanvas.gameObject.SetActive(true);
                timeManager.TV.gameObject.SetActive(true);

                timeManager.SetupTimer();
                emailGen.CloseReportCard();
                break;

            case GameState.PassiveGameplayState:
                UI_Manager.ActiveGameplayCanvas.gameObject.SetActive(false);
                UI_Manager.PassiveGameplayCanvas.gameObject.SetActive(true);
                timeManager.TV.gameObject.SetActive(true);

                timeManager.SetupTimer();
                emailGen.CloseReportCard();
                break;

            case GameState.CustomizeGameplayState:
                UI_Manager.ReportCanvas.gameObject.SetActive(false);
                UI_Manager.ActiveGameplayCanvas.gameObject.SetActive(false);
                UI_Manager.CustomizationCanvas.gameObject.SetActive(true);
                timeManager.TV.gameObject.SetActive(true);

                timeManager.SetupTimer();
                emailGen.CloseReportCard();
                break;

            case GameState.TutorialState:
                UI_Manager.TutorialCanvas.gameObject.SetActive(true);
                UI_Manager.MainMenuCanvas.gameObject.SetActive(false);
                timeManager.TV.gameObject.SetActive(false);
                audioManager.Playbackground();
                break;

            case GameState.ReportState:
                UI_Manager.ReportCanvas.gameObject.SetActive(true);
                UI_Manager.ActiveGameplayCanvas.gameObject.SetActive(false);

                timeManager.TV.gameObject.SetActive(true);

                timeManager.SetupTimer();
                emailGen.GenerateReportCard();
                break;
        }

        if (Bootup)
        {
            OldState(oldState);
        }

        Bootup = true;

        onGameStateChanged?.Invoke(newState);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (UI_Manager.ActiveGameplayCanvas.gameObject.activeInHierarchy)
                UI_Manager.ActiveGameplayCanvas.gameObject.SetActive(false);
            else
                UI_Manager.ActiveGameplayCanvas.gameObject.SetActive(true);

        }
    }

    private void OldState(GameState oldState)
    {
        switch (oldState)
        {
            case GameState.MainMenuState:
                UI_Manager.MainMenuCanvas.gameObject.SetActive(false);
                break;

            case GameState.ActiveGameplayState:
                UI_Manager.ActiveGameplayCanvas.gameObject.SetActive(false);
                break;
            
            case GameState.PassiveGameplayState:
                UI_Manager.PassiveGameplayCanvas.gameObject.SetActive(false);
                break;
            
            case GameState.CustomizeGameplayState:
                UI_Manager.CustomizationCanvas.gameObject.SetActive(false);
                break;
           
            case GameState.TutorialState:
                break;
        }
    }
}

public enum GameState
{
    MainMenuState,
    ActiveGameplayState,
    PassiveGameplayState,
    CustomizeGameplayState,
    PlacementState,
    TutorialState,
    ReportState
}