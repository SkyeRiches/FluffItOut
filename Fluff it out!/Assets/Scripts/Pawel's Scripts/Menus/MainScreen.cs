using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour {
    PlayerControls controls;

    /// <summary>
    /// upon the object being set active a new instance of the controls system is created
    /// when certain buttons are pressed, certain functions are called
    /// </summary>
    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Attack.performed += ctx => PlayGame();
        controls.Gameplay.Grenade.performed += ctx => PlayGame();
        controls.Gameplay.Jump.performed += ctx => PlayGame();
        controls.Gameplay.Melee.performed += ctx => PlayGame();
        controls.Gameplay.MenuDown.performed += ctx => PlayGame();
        controls.Gameplay.MenuLeft.performed += ctx => PlayGame();
        controls.Gameplay.MenuRight.performed += ctx => PlayGame();
        controls.Gameplay.MenuUp.performed += ctx => PlayGame();
        controls.Gameplay.Pause.performed += ctx => PlayGame();
        controls.Gameplay.Pickup.performed += ctx => PlayGame();
        controls.Gameplay.Reload.performed += ctx => PlayGame();
        controls.Gameplay.Scoreboard.performed += ctx => PlayGame();
        controls.Gameplay.LockIn.performed += ctx => PlayGame();
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
    /// loads the main game scene as active
    /// </summary>
    public void PlayGame() {
        SceneManager.LoadScene(1);
        EndGameManager.GameIsOver = false;
    }

}
