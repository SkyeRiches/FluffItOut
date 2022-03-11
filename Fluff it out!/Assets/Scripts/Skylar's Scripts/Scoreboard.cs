using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the scoreboard in the game
/// </summary>
public class Scoreboard : MonoBehaviour {

    [SerializeField]
    private GameObject playerManager;

    Dictionary<string, int> leaderBoard;

    string[] scoreOrder;

    /// <summary>
    /// upon the game starting a new dictionary for the leaderboard is created
    /// and a new element in the dictionary is added for each player in the game
    /// it then  creates an array to store player names in from the length of the dictionary
    /// </summary>
    void Awake() {
        leaderBoard = new Dictionary<string, int>();

        for (int i = 1; i < IdentityPlayer.playerNumber; i++) {
            leaderBoard.Add("Player " + i.ToString(), 0);
        }

        scoreOrder = new string[leaderBoard.Count];
    }

    /// <summary>
    /// Update calls functions that need to be called in each frame of the game loop
    /// </summary>
    // Update is called once per frame
    void Update() {
        UpdateScores();

        OrderScores();

        DisplayScores();
    }

    /// <summary>
    /// this function will loop through the leaderboard dictionary and will get the current score from each player and store it with the corresponding id
    /// e.g. player 1 score goes in leaderboard[player 1]
    /// </summary>
    private void UpdateScores() {
        for (int j = 1; j <= leaderBoard.Count; j++) {
            string key = "Player " + j.ToString();
            leaderBoard[key] = GameObject.Find(key).GetComponent<Scoring>().currentScore;
        }
    }

    /// <summary>
    /// this function uses a bubble sort to order an array of the player neames in order of who has the highest score
    /// </summary>
    private void OrderScores() {
        
        for (int j = 0; j < scoreOrder.Length; j++) {
            scoreOrder[j] = "Player " + (j + 1).ToString();
        }

        string temporary;

        // Bubble sort
        for (int k = 0; k <= scoreOrder.Length - 2; k++)
        {
            for (int i = 0; i <= scoreOrder.Length - 2; i++)
            {
                if (leaderBoard[scoreOrder[i]] < leaderBoard[scoreOrder[i + 1]])
                {
                    temporary = scoreOrder[i + 1];
                    scoreOrder[i + 1] = scoreOrder[i];
                    scoreOrder[i] = temporary;
                }
            } 
        }
    }

    /// <summary>
    /// this will loop through all of the items in the array of names that have been ordered by score
    /// it will then get the next text object ui on the score board and display the current item of the array it is on (the current player name)
    /// it will then go on to display the score of that player in the next score text object
    /// </summary>
    private void DisplayScores() {
        for (int i = 0; i < scoreOrder.Length; i++) {
            string key = "PlayerName" + (i + 1).ToString();
            GameObject textField = GameObject.Find(key);
            textField.GetComponent<Text>().text = scoreOrder[i];

            string key2 = "PlayerScore" + (i + 1).ToString();
            GameObject textScore = GameObject.Find(key2);
            textScore.GetComponent<Text>().text = leaderBoard[scoreOrder[i]].ToString();
        }
    }
}
