using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    PlayerControls controls;

    private int menuSelection = 0;

    [SerializeField]
    private GameObject playSelected;
    [SerializeField]
    private GameObject optionsSelected;
    [SerializeField]
    private GameObject quitSelected;

    public GameObject OptionsMenuUI;
    public GameObject MainMenuUI;

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
            menuSelection = 2;
        } else {
            menuSelection -= 1;
        }
    }

    /// <summary>
    /// when menu up button is pressed, increase the menu selection by 1
    /// </summary>
    void MainIncrementOption() {
        if (menuSelection == 2) {
            menuSelection = 0;
        } else {
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
            quitSelected.SetActive(false);
        } else if (menuSelection == 1) {
            playSelected.SetActive(false);
            optionsSelected.SetActive(true);
            quitSelected.SetActive(false);
        } else if (menuSelection == 2) {
            quitSelected.SetActive(true);
            playSelected.SetActive(false);
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
                    Options();
                    break;
                }
            case 2: {
                    QuitGame();
                    break;
                }
            default: {
                    break;
                }
        }
    }

    /// <summary>
    /// loads the main game scene as active
    /// </summary>
    public void PlayGame() {
        SceneManager.LoadScene(1);
        EndGameManager.GameIsOver = false;
    }

    public void Options() {
        OptionsMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }



    /// <summary>
    /// closes the game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
