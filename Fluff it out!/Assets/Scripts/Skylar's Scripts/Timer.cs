using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the timer for the game, the timer counts down as time goes on until the players run out of time
/// </summary>
public class Timer : MonoBehaviour {
    bool gameHasStarted = false;
    public static float currentTime = 0f;
    public static float startingTime = 300f;

    [SerializeField]
    Text countdownText;

    /// <summary>
    /// upon start the variables will be assigned their default values
    /// </summary>
    // Start is called before the first frame update
    void Start() {
        gameHasStarted = true;
        currentTime = startingTime;
        EndGameManager.GameIsOver = false;
    }

    /// <summary>
    /// once the game has started, the timer is decreased by 1 each second, then it is written to the text ui for the timer
    /// if the timer hits 0 then the game will be set as being over, where the endgame manager will take over from there
    /// If the timer is below 30 then the color of the timer text will change to warn the players that they are in their last segment of time now
    /// </summary>
    // Update is called once per frame
    void Update() {
        if (gameHasStarted) {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("00");
            if (currentTime <= 0f) {
                EndGameManager.GameIsOver = true;
                currentTime = 0f;
                gameHasStarted = false;
            } else {
                //EndGameMenu.GameIsOver = false;
                gameHasStarted = true;
            }
        }
        if (currentTime < 30) {
            countdownText.color = Color.red;
        }
    }

}
