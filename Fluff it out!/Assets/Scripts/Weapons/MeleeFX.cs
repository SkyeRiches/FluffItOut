using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFX : MonoBehaviour {

    private float damage = 30;
    public float swingSpeed;
    private float moveSlow;
    private float damageOT = 0;
    private float bleedDuration = 2;

    private string playerHolding;

    // calls the intialisation of the melee weapons
    private void Awake() {
        MeleeInit();
    }

    // sets which player is holding the weapons this is so that they dont accidentally damage themselves when slashing
    public void SetPlayer(string player) {
        playerHolding = player;
    }

    // Intialises the values for each melee weapon
    private void MeleeInit() {
        string name = gameObject.name;

        switch (name) {
            case "Squeaky Hammer":
                damage = 30;
                swingSpeed = 1f;
                moveSlow = 1f;
                damageOT = 0f;
                break;

            case "Spoon":
                damage = 30;
                swingSpeed = 1f;
                moveSlow = 1f;
                damageOT = 0f;
                break;

            case "Lollipop":
                damage = 30;
                swingSpeed = 1f;
                moveSlow = 0.5f;
                damageOT = 0f;
                break;

            case "Needle":
                damage = 15;
                swingSpeed = 1f;
                moveSlow = 1f;
                damageOT = 2f;
                break;

            case "Scissor":
                damage = 15;
                swingSpeed = 1f;
                moveSlow = 1f;
                damageOT = 0f;
                break;

            default:
                break;
        }
    }

    // when the hammer collides with something it will damage the palyer and trigger the special effect of the weapon
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" & other.name != playerHolding) {

            GameObject player = GameObject.Find(playerHolding);
            player.GetComponent<Rumble>().RumbleConstant(0.5f, 0.5f, 0.3f);

            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            MeleeAttack(other.gameObject);
        }
    }

    // Triggers the special fx of the weapons
    public void MeleeAttack(GameObject target) {
        string name = gameObject.name;

        switch (name) {
            case "Squeaky Hammer":
                break;

            case "Lollipop":
                Lollipop(target);
                break;

            case "Needle":
                Needle(target);
                break;

            case "Scissor":
                Needle(target);
                break;

            default:
                break;
        }
    }

    // THis slows the target hit so it moves slower
    private void Lollipop(GameObject target) {
        target.GetComponent<PlayerMovement>().SlowMovement(moveSlow);
    }

    // makes the target bleed
    private void Needle(GameObject target) {
        target.GetComponent<PlayerHealth>().Bleed(damageOT, bleedDuration);
    }
}
