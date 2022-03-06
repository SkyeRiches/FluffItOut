using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will be attatched to objects that will be used to heal the player with a certain amount of health
/// </summary>
public class HealingScript : MonoBehaviour
{
    public float heal = 10f;

    /// <summary>
    /// if the player enters teh trigger collider on this object, it will call the healing function on the player's health script
    /// it will then increase the players health by the amount of health passed through the parameter of the heal function
    /// it then destroys this object so that it is one time use
    /// </summary>
    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().HealDamage(heal);
            Destroy(gameObject);
        }
    }
}
