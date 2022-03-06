using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class TheCollector : MonoBehaviour {

    private GunWeapons weapon;
    [SerializeField]
    private Text ammoDisplay;
    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private Animator playerAnim;

    private float damage;
    private float range;
    private float fireRate;
    private int ammo;
    private float healPerShot;
    private float currentAmmo;

    private bool readyToFire = true;
    private bool fired = false;

    private GameObject shotTarget;

    private void OnEnable() {
        weapon = gameObject.GetComponentInChildren<GunWeapons>();

        damage = weapon.damage;
        range = weapon.range;
        fireRate = weapon.fireRate;
        ammo = weapon.ammo;
        healPerShot = weapon.healPerShot;

        fired = false;

        currentAmmo = ammo;
    }

    /// <summary>
    /// updates the ui text to display the current ammo
    /// </summary>
    // Update is called once per frame
    private void Update() {
        ammoDisplay.text = currentAmmo.ToString();

        RaycastHit hitConst;
        // creates a ray cast that checks for hits 
        Ray rayConst = new Ray(crosshair.transform.position, crosshair.transform.forward);
        if (Physics.Raycast(rayConst, out hitConst)) {
            shotTarget = hitConst.collider.gameObject;
        }
    }

    /// <summary>
    /// when the attack trigger is pressed on the controller,
    /// the gun will fire as long as the gun ahs ammo, the player isnt in grace period, and the guns's script is enabled
    /// </summary>
    public void Attack() {
        if (gameObject.GetComponent<PlayerHealth>().isGracePeriod == false && gameObject.GetComponent<TheCollector>().enabled) {

            if (!fired) {
                gameObject.GetComponent<Rumble>().RumbleLinear(0.1f, 1f, 0.1f, 1f, 10);
                fired = true;
            }

            weapon.GetComponentInChildren<VisualEffect>().Play();

            while (readyToFire && currentAmmo > 0) {

                RaycastHit hit;
                // creates a ray cast that checks for hits 
                Ray ray = new Ray(crosshair.transform.position, crosshair.transform.forward);

                if (Physics.Raycast(ray, out hit, range)) {
                    GameObject target = hit.collider.gameObject;
                    // if a player is hit, they take damage and the name of who shot them is passed onto their health script
                    if (target.GetComponent<PlayerHealth>() != null) {
                        target.GetComponent<PlayerHealth>().TakeDamage(damage);
                        target.GetComponent<PlayerHealth>().killedBy = gameObject.name;
                        gameObject.GetComponent<PlayerHealth>().HealDamage(healPerShot);
                    }
                }
                currentAmmo--;
                
                if (currentAmmo <= 0) {
                    ammoDisplay.text = "0";
                    gameObject.GetComponent<PickUpController>().Drop();
                }

                readyToFire = false;
                StartCoroutine(SemiAuto());
            }


        }
    }

    // starts a count down before the next shot can be fired
    private IEnumerator SemiAuto() {
        yield return new WaitForSeconds(fireRate);
        readyToFire = true;
        Attack();
    }
}
