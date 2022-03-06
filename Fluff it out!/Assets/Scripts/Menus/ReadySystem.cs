using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// This is the main class that controls the ready up system
/// </summary>
public class ReadySystem : MonoBehaviour {

    [SerializeField]
    private Image player1Ready;
    [SerializeField]
    private Image player2Ready;
    [SerializeField]
    private Image player3Ready;
    [SerializeField]
    private Image player4Ready;

    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private GameObject scoreBoard;
    [SerializeField]
    private GameObject cinematic;

    public bool isReady1 = false;
    public bool isReady2 = false;
    public bool isReady3 = false;
    public bool isReady4 = false;

    /// <summary>
    /// Time is set to not pass during this screen
    /// </summary>
    void Start() {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Depending on which players have clicked ready, the color of the ui squares will be changed so that the ready players have green squares
    /// this is only temporary until we have a finalised version of the ready system
    /// </summary>
    // Update is called once per frame
    void Update() {

        if (isReady1) {
            player1Ready.color = Color.green;
        }
        else {
            player1Ready.color = Color.red;
        }

        if (isReady2) {
            player2Ready.color = Color.green;
        }
        else {
            player2Ready.color = Color.red;
        }

        if (isReady3) {
            player3Ready.color = Color.green;
        }
        else {
            player3Ready.color = Color.red;
        }

        if (isReady4) {
            player4Ready.color = Color.green;
        }
        else {
            player4Ready.color = Color.red;
        }
    }

    /// <summary>
    /// if player 1 presses the button to start then the timescale will be set to normal time (1), 
    /// and the ready screen will be disabled along with the game timer being started
    /// </summary>
    public void StartGame(string player) {
        // Only player 1 can start the game 
        if (player == "Player 1") {
            Time.timeScale = 1f;
            cinematic.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// When the player's press the ready button, it will call this and then depending on which player it was,
    /// the appropriate bool will be set as true to indicate that they have readied up.
    /// </summary>
    public void ReadyPlayer(string player) {
        switch (player) {

            case "Player 1": {
                isReady1 = true;
                break;
            }

            case "Player 2": {
                isReady2 = true;
                break;
            }

            case "Player 3": {
                isReady3 = true;
                break;
            }

            case "Player 4": {
                isReady4 = true;
                break;
            }

            default: {
                break;
            }
        }
    }

    /// <summary>
    /// When the player's press the ready button, it will call this and then depending on which player it was,
    /// the appropriate bool will be set as false to indicate that they have cancelled the ready 
    /// </summary>
    public void UnReadyPlayer(string player) {
        switch (player) {

            case "Player 1": {
                isReady1 = false;
                break;
            }

            case "Player 2": {
                isReady2 = false;
                break;
            }

            case "Player 3": {
                isReady3 = false;
                break;
            }

            case "Player 4": {
                isReady4 = false;
                break;
            }

            default: {
                break;
            }
        }
    }
}
