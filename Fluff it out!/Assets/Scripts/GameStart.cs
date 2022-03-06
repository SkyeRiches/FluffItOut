using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    [SerializeField]
    private GameObject barriers;

    [SerializeField]
    private Text countdownText;
    private float currentTime = 0f;
    private float startingTime = 5f;

    private void OnEnable() {
        currentTime = startingTime;
    }

    /// <summary>
    /// after 5 seconds the barriers infornt of the spawn points disappear adn the timer gets enabled
    /// </summary>
    void Update() {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("00");

        if (currentTime <= 0) {
            barriers.SetActive(false);
            gameObject.GetComponent<Timer>().enabled = true;
        }
    }
}
