using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {
    PlayerControls controls;

    private int menuSelection = 0;

    [SerializeField]
    private GameObject playSelected;
    [SerializeField]
    private GameObject optionsSelected;
    [SerializeField]
    private GameObject menuSelected;
    [SerializeField]
    private GameObject quitSelected;

    /// <summary>
    /// upon the object being set active a new instance of the controls system is created
    /// when certain buttons are pressed, certain functions are called
    /// </summary>
    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.MenuDown.performed += ctx => MainIncrementOption();
        controls.Gameplay.MenuUp.performed += ctx => MainDecrementOption();
        controls.Gameplay.Jump.performed += ctx => MainSelectOption();
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
    /// when menu down button is pressed, decrease the menu selection by 1
    /// </summary>
    void MainDecrementOption() {
        if (menuSelection == 0) {
            menuSelection = 3;
        }
        else {
            menuSelection -= 1;
        }
    }

    /// <summary>
    /// when menu up button is pressed, increase the menu selection by 1
    /// </summary>
    void MainIncrementOption() {
        if (menuSelection == 3) {
            menuSelection = 0;
        }
        else {
            menuSelection += 1;
        }
    }

    /// <summary>
    /// sets the highlight behind the currently selected option as active to give the user indication of which is selected
    /// </summary>
    void Update() {
        if (menuSelection == 0) {
            playSelected.SetActive(true);
            optionsSelected.SetActive(false);
            menuSelected.SetActive(false);
            quitSelected.SetActive(false);
        }
        else if (menuSelection == 1) {
            playSelected.SetActive(false);
            optionsSelected.SetActive(true);
            menuSelected.SetActive(false);
            quitSelected.SetActive(false);
        }
        else if (menuSelection == 2) {
            playSelected.SetActive(false);
            optionsSelected.SetActive(false);
            menuSelected.SetActive(true);
            quitSelected.SetActive(false);
        }
        else if (menuSelection == 3) {
            quitSelected.SetActive(true);
            playSelected.SetActive(false);
            menuSelected.SetActive(false);
            optionsSelected.SetActive(false);
        }
    }

    /// <summary>
    /// when the 'a' button is pressed on the controller, 
    /// this checks the currently selected menu option and will  execute the right function
    /// </summary>
    void MainSelectOption() {
        switch (menuSelection) {
            case 0: {
                PlayGame();
                break;
            }
            case 1: {
                Debug.Log("loading options");
                break;
            }
            case 2: {
                LoadMenu();
                break;
            }
            case 3: {
                QuitGame();
                break;
            }
            default: {
                break;
            }
        }
    }

    /// <summary>
    /// loads the main game scene to play again
    /// </summary>
    public void PlayGame() {
        SceneManager.LoadScene(2);
        EndGameManager.GameIsOver = false;
    }

    /// <summary>
    /// loads the main menu scene
    /// </summary>
    public void LoadMenu() {
        SceneManager.LoadScene(0);
        EndGameManager.GameIsOver = false;
    }

    /// <summary>
    /// closes the game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
