using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputManager : MonoBehaviour
{
    private EmailCreator email;

    [SerializeField]
    private Canvas activeGameplayCanvas, idleGameplayCanvas, customizationCanvas, settingsCanvas, shopCanvas, mainMenuCanvas, inventoryCanvas, clothesCanvas, tutorialCanvas, reportCanvas, leaderboardCanvas, achievementCanvas;
    public Canvas ActiveGameplayCanvas
    {
        get { return activeGameplayCanvas; }
        set { activeGameplayCanvas = value; }
    }
    public Canvas PassiveGameplayCanvas
    {
        get { return idleGameplayCanvas; }
    }
    public Canvas CustomizationCanvas
    {
        get { return customizationCanvas; }
    }
    public Canvas MainMenuCanvas
    {
        get { return mainMenuCanvas; }
    }
    public Canvas SettingsCanvas
    {
        get { return settingsCanvas; }
    }
    public Canvas ShopCanvas
    {
        get { return shopCanvas; }
    }
    public Canvas InventoryCanvas
    {
        get { return inventoryCanvas; }
    }
    public Canvas ClothesCanvas
    {
        get { return clothesCanvas; }
    }
    public Canvas TutorialCanvas
    {
        get { return tutorialCanvas; }
    }
    public Canvas ReportCanvas
    {
        get { return reportCanvas; }
    }
    public Canvas LeaderboardCanvas
    { get { return leaderboardCanvas; } }

    public Canvas AchievementCanvas
    {
        get { return achievementCanvas; }
    }

    private Canvas LastCanvas;


    [SerializeField]
    private List<Button> buttons = new List<Button>();
    private bool openedMenu = false;

    private bool openedSettings = false;
    private bool otherCanvas;

    // ### UI METHODS ### \\

    private void Start()
    {
        email = FindObjectOfType<EmailCreator>();

    }


    public void GameplayButton(bool value) // bool true means: Interact, false = Delete
    {
        GameManager.Instance.audioManager.PlayButtonSound();
        email.CheckEmail(value); // Calls email to check the value
    }

    public void StartGame()
    {
        GameManager.Instance.SwitchState(GameState.TutorialState);
    }

    public void StartActiveGameplay()
    {
        if (GameManager.Instance.gameState == GameState.TutorialState)
            tutorialCanvas.gameObject.SetActive(false);

        GameManager.Instance.SwitchState(GameState.ActiveGameplayState);
    }

    public void CloseGame()
    {
        // Save game

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //public void OpenMenu()
    //{
    //    if (!openedMenu)
    //        openedMenu = true;
    //    else
    //        openedMenu = false;

    //    for (int i = 0; i < buttons.Count; i++)
    //    {
    //        buttons[i].gameObject.SetActive(openedMenu);
    //    }
    //}

    public void OpenStoreMenu(bool activate)
    {
        if (activate)
        {
            customizationCanvas.gameObject.SetActive(false);
            FindObjectOfType<ItemHandeler>().OpenShop(1); // ID set to 1 to open the base set.
        }
        else
        {
            customizationCanvas.gameObject.SetActive(true);
        }
        shopCanvas.gameObject.SetActive(activate);
    }

    public void OpenAchievementCanvas(bool activate)
    {
        CloseOldCanvas(activate);
        achievementCanvas.gameObject.SetActive(activate);
    }

    public void OpenLeaderboardCanvas(bool activate)
    {
        CloseOldCanvas(activate);
        leaderboardCanvas.gameObject.SetActive(activate);
    }

    public void OpenClothingCanvas(bool activate)
    {
        CloseOldCanvas(activate);
        clothesCanvas.gameObject.SetActive(activate);
    }

    public void OpenInventory(bool activate)
    {
        if (activate)
        {
            customizationCanvas.gameObject.SetActive(false);
        }
        else
        {
            customizationCanvas.gameObject.SetActive(true);
            FindObjectOfType<ItemHandeler>().OpenInventory(1);
        }
        inventoryCanvas.gameObject.SetActive(activate);
    }

    private void CloseOldCanvas(bool activate)
    {
        if (activate)
        {
            customizationCanvas.gameObject.SetActive(false);
        }
        else
        {
            customizationCanvas.gameObject.SetActive(true);
        }
    }

    public void PlaceObject()
    {
        inventoryCanvas.gameObject.SetActive(false);
        customizationCanvas.gameObject.SetActive(true);
    }

    private void LastOpenedCanvas(Canvas canvas)
    {
        LastCanvas = canvas;
    }


    public void SettingsMenu(bool activate)
    {
        if (activate)
            LastCanvas.gameObject.SetActive(false);
        else
            LastCanvas.gameObject.SetActive(true);

        settingsCanvas.gameObject.SetActive(activate);
    }
}