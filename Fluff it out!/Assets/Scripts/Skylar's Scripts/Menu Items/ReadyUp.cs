using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class will mark the player as ready when they press the left button on their controller (same one used to reload)
/// </summary>
public class ReadyUp : MonoBehaviour {

    private bool isLockedIn = false;

    private GameObject readySystem;

    [SerializeField]
    private GameObject[] charSkins = new GameObject[6];

    private int charChoice;

    /// <summary>
    /// Displays the image of the character they have chosen
    /// </summary>
    private void Update() {
        if (name == "Player 1") {
            charChoice = readySystem.GetComponent<ChooseCharacter>().p1Choice;
        }
        else if (name == "Player 2") {
            charChoice = readySystem.GetComponent<ChooseCharacter>().p2Choice;
        }
        else if (name == "Player 3") {
            charChoice = readySystem.GetComponent<ChooseCharacter>().p3Choice;
        }
        else if (name == "Player 4") {
            charChoice = readySystem.GetComponent<ChooseCharacter>().p4Choice;
        }
    }

    /// <summary>
    /// assigns the readyscreen to be the object that has the ready up system on it
    /// </summary>
    private void Awake() {
        readySystem = GameObject.Find("ReadyScreen");
    }

    /// <summary>
    /// when the player presses the left button it will inform the ready system that the player is in the game so it will create a player for them
    /// </summary>
    private void OnReload() {
        if (isLockedIn){
            charSkins[charChoice].SetActive(false);
            readySystem.GetComponent<ReadySystem>().UnReadyPlayer(name);
            isLockedIn = false;
        }
        else if(!isLockedIn){
            charSkins[charChoice].SetActive(true);
            readySystem.GetComponent<ReadySystem>().ReadyPlayer(name);
            isLockedIn = true;
        }
    }

    /// <summary>
    /// when the player presses the same button as jump, it will inform the ready system to start, 
    /// however the ready system then checks which player pressed it as only player 1 is allowed to start the game
    /// </summary>
    private void OnJump() {
        readySystem.GetComponent<ReadySystem>().StartGame(name);
    }

    /// <summary>
    /// if they press up on d pad, choice is incremented
    /// </summary>
    private void OnMenuUp() {
        if(!isLockedIn){
            readySystem.GetComponent<ChooseCharacter>().IncrementChoice(name);  
        }
        
    }

    /// <summary>
    /// if they press down on dpad, choice is decremented
    /// </summary>
    private void OnMenuDown() {
        if (!isLockedIn){
            readySystem.GetComponent<ChooseCharacter>().DecrementChoice(name);
        }
    }
}
