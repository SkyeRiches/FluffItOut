using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMelee : MonoBehaviour {

    public bool canAttack = true;
    private float swingSpeed;
    [SerializeField]
    private Animator playerAnim;

    private void Update() {
        // This is just so the script can be disabled
    }


    // When the player equips the weapon it will set some values to the initial value defined in melee fx
    public void OnEquipped() {
        gameObject.GetComponentInChildren<MeleeFX>().SetPlayer(name);
        swingSpeed = gameObject.GetComponentInChildren<MeleeFX>().swingSpeed;
    }

    // When you swing the weapon, it will play the animation for swinging
    private void OnAttack() {
        if (canAttack && gameObject.GetComponent<BaseMelee>().enabled) {
            canAttack = false;
            playerAnim.SetBool("Attacking", true);

            StartCoroutine(SwingCooldown(swingSpeed));
        }
    }

    // Waits for attack cooldown to end to set attack as true again
    public IEnumerator SwingCooldown(float cooldown) {
        yield return new WaitForSeconds(2);
        playerAnim.SetBool("Attacking", false);
        canAttack = true;
    }
}
