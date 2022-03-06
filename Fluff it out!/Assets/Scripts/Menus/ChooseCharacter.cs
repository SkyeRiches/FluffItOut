using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour {

    public int p1Choice = 0;
    public int p2Choice = 0;
    public int p3Choice = 0;
    public int p4Choice = 0;

    [SerializeField]
    private GameObject[] p1Images = new GameObject[6];
    [SerializeField]
    private GameObject[] p2Images = new GameObject[6];
    [SerializeField]
    private GameObject[] p3Images = new GameObject[6];
    [SerializeField]
    private GameObject[] p4Images = new GameObject[6];


    /// <summary>
    /// If they press the up on d pad, their choice of cahracter will increase
    /// </summary>
    /// <param name="player"></param>
    public void IncrementChoice(string player) {
        switch (player) {

            case "Player 1": {
                    if (p1Choice == 5) {
                        p1Choice = 0;
                    }
                    else {
                        p1Choice++;
                    }
                    break;
                }

            case "Player 2": {
                    if (p2Choice == 5) {
                        p2Choice = 0;
                    }
                    else {
                        p2Choice++;
                    }
                    break;
                }

            case "Player 3": {
                    if (p3Choice == 5) {
                        p3Choice = 0;
                    }
                    else {
                        p3Choice++;
                    }
                    break;
                }

            case "Player 4": {
                    if (p4Choice == 5) {
                        p4Choice = 0;
                    }
                    else {
                        p4Choice++;
                    }
                    break;
                }

            default: {
                    break;
                }
        }
    }

    /// <summary>
    /// If they press the down on d pad, their choice of cahracter will decrease
    /// </summary>
    public void DecrementChoice(string player) {
        switch (player) {

            case "Player 1": {
                    if (p1Choice == 0) {
                        p1Choice = 5;
                    }
                    else {
                        p1Choice--;
                    }
                    break;
                }

            case "Player 2": {
                    if (p2Choice == 0) {
                        p2Choice = 5;
                    }
                    else {
                        p2Choice--;
                    }
                    break;
                }

            case "Player 3": {
                    if (p3Choice == 0) {
                        p3Choice = 5;
                    }
                    else {
                        p3Choice--;
                    }
                    break;
                }

            case "Player 4": {
                    if (p4Choice == 0) {
                        p4Choice = 5;
                    }
                    else {
                        p4Choice--;
                    }
                    break;
                }

            default: {
                    break;
                }
        }
    }

    /// <summary>
    /// Depending on character choice the appropriate character model will be set active
    /// </summary>
    private void Update() {
        //array of the iamges and it checks value
        for (int i = 0; i < 6; i++) {
            if (i == p1Choice) {
                p1Images[i].SetActive(true);
            }
            else {
                p1Images[i].SetActive(false);
            }

            if (i == p2Choice) {
                p2Images[i].SetActive(true);
            }
            else {
                p2Images[i].SetActive(false);
            }

            if (i == p3Choice) {
                p3Images[i].SetActive(true);
            }
            else {
                p3Images[i].SetActive(false);
            }

            if (i == p4Choice) {
                p4Images[i].SetActive(true);
            }
            else {
                p4Images[i].SetActive(false);
            }
        }
    }
}
