using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains a number that gets incremented when a new player joins the game,
/// it is used to assign the right id to each player
/// </summary>
public class IdentityPlayer : MonoBehaviour {

    public static int playerNumber;

    // Start is called before the first frame update
    void Start() {
        playerNumber = 1;
    }
}
