using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is called upon the player first spawning into the game
/// </summary>
public class GetIdentity : MonoBehaviour {

    [SerializeField]
    GameObject render;

    /// <summary>
    /// when the player spawns in for the first time, the name of the player will be changed to abide by the current id in the IdentityPlayer script
    /// it also changes the material of the player to be the material that has the same name as the player
    /// </summary>
    private void Awake() {
        gameObject.name = "Player " + IdentityPlayer.playerNumber;
        IdentityPlayer.playerNumber++;
        
    }
}
