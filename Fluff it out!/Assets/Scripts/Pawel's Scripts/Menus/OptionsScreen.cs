using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreen : MonoBehaviour {

    private static bool GameIsPaused = false;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject optionsMenuUI;
    [SerializeField]
    private GameObject audioMenu;
    [SerializeField]
    private GameObject controlsMenu;
    [SerializeField]
    private GameObject mainCanvas;

    [SerializeField]
    private GameObject audioSelected;
    [SerializeField]
    private GameObject controlsSelected;
    [SerializeField]
    private GameObject backSelected;

    PlayerControls controls;

    private int menuSelection = 0;

    /// <summary>
    /// upon the object being set active a new instance of the controls system is created
    /// when certain buttons are pressed, certain functions are called
    /// </summary>
    void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Pause.performed += ctx => Pause();
        controls.Gameplay.MenuDown.performed += ctx => DecrementOption();
        controls.Gameplay.MenuUp.performed += ctx => IncrementOption();
        controls.Gameplay.Jump.performed += ctx => SelectOption();
    }

    /// <summary>
    /// when the script is enabled, so is the controls
    /// </summary>
    void OnEnable() {
        controls.Gameplay.Enable();
    }

    /// <summary>
    /// when the script is disabled, so is the controls
    /// </summary>
    void OnDisable() {
        controls.Gameplay.Disable();
    }

    /// <summary>
    /// goes back to the main pause menu and sets this menu as inactive
    /// </summary>
    void Pause() {
        pauseMenuUI.SetActive(true);
        
        GameIsPaused = true;
        mainCanvas.GetComponent<PauseMenuManager>().enabled = true;
        gameObject.GetComponent<OptionsScreen>().enabled = false;
        optionsMenuUI.SetActive(false);
    }

    /// <summary>
    /// when menu down button is pressed, decrease the menu selection by 1
    /// </summary>
    private void DecrementOption() {
        if (menuSelection == 2) {
            menuSelection = 0;
        }
        else {
            menuSelection += 1;
        }
    }

    /// <summary>
    /// when menu up button is pressed, increase the menu selection by 1
    /// </summary>
    private void IncrementOption() {
        if (menuSelection == 0) {
            menuSelection = 2;
        }
        else {
            menuSelection -= 1;
        }

    }

    /// <summary>
    /// when the 'a' button is pressed on the controller, 
    /// this checks the currently selected menu option and will  execute the right function
    /// </summary>
    void SelectOption() {
        switch (menuSelection) {
            case 0: {
                    AudioMenu();
                    break;
                }
            case 1: {
                    ControlsMenu();
                    break;
                }
            case 2: {
                    Pause();
                    break;
                }
            default: {
                    break;
                }
        }
    }

    /// <summary>
    /// sets the audio options ui and script as active, sets options screen ui as inactive
    /// </summary>
    public void AudioMenu() {
        audioMenu.SetActive(true);
        audioMenu.GetComponent<AudioOptions>().enabled = true;
        optionsMenuUI.SetActive(false);
    }

    /// <summary>
    /// sets the controls options ui and script as active, sets options screen ui as inactive
    /// </summary>
    private void ControlsMenu() {
        controlsMenu.SetActive(true);
        controlsMenu.GetComponent<ControlsOptions>().enabled = true;
        optionsMenuUI.SetActive(false);
    }


    /// <summary>
    /// sets the highlight behind the currently selected option as active to give the user indication of which is selected
    /// </summary>
    void Update() {
        if (menuSelection == 0) {
            audioSelected.SetActive(true);
            controlsSelected.SetActive(false);
            backSelected.SetActive(false);
        }
        else if (menuSelection == 1) {
            controlsSelected.SetActive(true);
            audioSelected.SetActive(false);
            backSelected.SetActive(false);
        }
        else if (menuSelection == 2) {
            backSelected.SetActive(true);
            audioSelected.SetActive(false);
            controlsSelected.SetActive(false);
        }
    }
}
