using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

    // If the player enters the area, they will die
    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(100);
        }
    }
}
