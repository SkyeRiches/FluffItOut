using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is attatched to the player and will be used to modify the score when a player gets a kill
/// </summary>
public class Scoring : MonoBehaviour {

    public int currentScore;
    public int streak;

    /// <summary>
    /// sets initial score to 0 as default so no player starts with bonus score
    /// </summary>
    // Start is called before the first frame update
    void Start() {
        currentScore = 0;
        streak = 0;
    }

    /// <summary>
    /// if the score is equal to that needed to win the game then the game will be set as over
    /// the endgame manager then takes over from there
    /// </summary>
    // Update is called once per frame
    void Update() {
        if (currentScore == EndGameManager.winningScore) {
            EndGameManager.GameIsOver = true;
        }
    }

    /// <summary>
    /// this is called when the player gets a kill, it will increase the players score by 1
    /// </summary>
    public void IncrementScore() {
        currentScore++;
        streak++;

        // Add in call to voice line manager to play streak voice line
    }

    public void ResetStreak() {
        streak = 0;

        // Add in call to voice line manager to play streak end line if they were on a certain value
    }
}
