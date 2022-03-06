using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject OptionsMenuUI;
    [SerializeField]
    private GameObject optionsMain;

    [SerializeField]
    private GameObject scoreBoard;
    [SerializeField]
    private GameObject resumeSelected;
    [SerializeField]
    private GameObject optionsSelected;
    [SerializeField]
    private GameObject menuSelected;
    [SerializeField]
    private GameObject quitSelected;

    [SerializeField]
    private GameObject readySystem;

    PlayerControls controls;

    private int menuSelection = 0;

    /// <summary>
    /// upon the object being set active a new instance of the controls system is created
    /// when certain buttons are pressed, certain functions are called
    /// </summary>
    void Awake(){
        gameObject.GetComponent<TimeFreeze>().enabled = false;

        menuSelection = 0;

        controls = new PlayerControls();

        controls.Gameplay.Pause.performed += ctx => menuPause();
        controls.Gameplay.MenuDown.performed += ctx => DecrementOption();
        controls.Gameplay.MenuUp.performed += ctx => IncrementOption();
        controls.Gameplay.Jump.performed += ctx => SelectOption();
        controls.Gameplay.Scoreboard.started += ctx => ShowLeaderBoard();
        controls.Gameplay.Scoreboard.canceled += ctx => CloseLeaderBoard();
    }

    /// <summary>
    /// when the script is enabled, so is the controls
    /// </summary>
    void OnEnable(){
        controls.Gameplay.Enable();
    }

    /// <summary>
    /// when the script is disabled, so is the controls
    /// </summary>
    void OnDisable(){
        controls.Gameplay.Disable();
    }

    /// <summary>
    /// if game is already paused, resume, if not pause the game
    /// </summary>
    public void menuPause(){
        if (GameIsPaused) {
            Resume();
        } 
        else {
            Pause();
        }
    }

    /// <summary>
    /// set the menu as inactive, unpause the game and disable the timefreeze script
    /// </summary>
    public void Resume() {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        gameObject.GetComponent<TimeFreeze>().enabled = false;
    }

    /// <summary>
    /// set the menu as active, pasue the game, set the timefreeze script as enabled
    /// </summary>
    void Pause() {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        gameObject.GetComponent<TimeFreeze>().enabled = true;
    }

    /// <summary>
    /// when menu down button is pressed, decrease the menu selection by 1
    /// </summary>
    private void DecrementOption(){
        if (!readySystem.activeInHierarchy) {
            if (menuSelection == 3) {
                menuSelection = 0;
            }
            else {
                menuSelection += 1;
            }
        }
    }

    /// <summary>
    /// when menu up button is pressed, increase the menu selection by 1
    /// </summary>
    private void IncrementOption(){
        if (!readySystem.activeInHierarchy) {
            if (menuSelection == 0) {
                menuSelection = 3;
            }
            else {
                menuSelection -= 1;
            }
        }
    }

    /// <summary>
    /// when the 'a' button is pressed on the controller, 
    /// this checks the currently selected menu option and will  execute the right function
    /// </summary>
    void SelectOption(){
        switch (menuSelection)
        {
            case 0:{
                Resume();
                break;
            }
            case 1: {
                    OptionsMenu();
                break;
            }
            case 2:{
                LoadMainMenu();
                break;
            }
            case 3:{
                QuitGame();
                break;
            }
            default:{
                break;
            }
        }
    }

    /// <summary>
    /// loads the scene of the main menu
    /// </summary>
    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// loads the options screen, disables this script, enables the options script, and disables the pause menu gui
    /// </summary>
    public void OptionsMenu() {
        OptionsMenuUI.SetActive(true);
        optionsMain.SetActive(true);
        optionsMain.GetComponent<OptionsScreen>().enabled = true;
        gameObject.GetComponent<PauseMenuManager>().enabled = false;
        pauseMenuUI.SetActive(false);
    }

    /// <summary>
    /// when quit is selected, the game will close
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// sets the highlight behind the currently selected option as active to give the user indication of which is selected
    /// </summary>
    void Update(){
        if (menuSelection == 0){
            resumeSelected.SetActive(true);
            optionsSelected.SetActive(false);
            menuSelected.SetActive(false);
            quitSelected.SetActive(false);
        }
        else if (menuSelection == 1){
            resumeSelected.SetActive(false);
            optionsSelected.SetActive(true);
            menuSelected.SetActive(false);
            quitSelected.SetActive(false);
        }
        else if (menuSelection == 2){
            menuSelected.SetActive(true);
            resumeSelected.SetActive(false);
            optionsSelected.SetActive(false);
            quitSelected.SetActive(false);
        }
        else if (menuSelection == 3){
            quitSelected.SetActive(true);
            resumeSelected.SetActive(false);
            optionsSelected.SetActive(false);
            menuSelected.SetActive(false);
        }
    }

    /// <summary>
    /// sets the leaderboard as active
    /// </summary>
    private void ShowLeaderBoard() {
        scoreBoard.SetActive(true);
    }

    /// <summary>
    /// sets the leaderboard as inactive
    /// </summary>
    private void CloseLeaderBoard() {
        scoreBoard.SetActive(false);
    }
}
