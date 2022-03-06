using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls what happens when the game ends
/// </summary>
public class EndGameManager : MonoBehaviour {

    public static bool GameIsOver = false;
    public static int winningScore = 25;

    [SerializeField]
    private GameObject scoreboard;
    [SerializeField]
    private GameObject timer;


    // Update is called once per frame
    void Update() {
        if (GameIsOver == true) {
            StartCoroutine(Pause());
        }
    }

    /// <summary>
    /// when the game ends it will freeze time, and show the scoreboard so the players can see who has won,
    /// it will then wait for 5 seconds to give the players time to compare score and then load the endgame scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator Pause() {
        Time.timeScale = 0f;
        GameIsOver = true;
        scoreboard.SetActive(true);
        timer.SetActive(false);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
