using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class BaseGun : MonoBehaviour {

    //[SerializeField]
    //private GameObject protoBullet;

    private GunWeapons weapon;
    [SerializeField]
    private Text ammoDisplay;
    [SerializeField]
    private GameObject crosshair, hitInd;

    [SerializeField]
    private Animator playerAnim;

    private float damage;
    private float range;
    private float fireRate;
    private int ammo;
    private float reload;
    private float slowing; // needs to be between 0 and 1 as its a multiplier to the enemies speed
    private float slowLength;
    private float force;
    private float bleedDamage;
    private float bleedTime;
    private float healPerShot;
    private float currentAmmo;

    private bool readyToFire = true;


    private GameObject shotTarget;
    private bool isPressed = false;

    /// <summary>
    /// When the script is enabled, the weapons values are set to those determined in the GunWeapons script
    /// </summary>
    public void OnEquipped() {
        weapon = gameObject.GetComponentInChildren<GunWeapons>();
        weapon.OnSpawn();

        damage = weapon.damage;
        range = weapon.range;
        fireRate = weapon.fireRate;
        ammo = weapon.ammo;
        currentAmmo = ammo;
        reload = weapon.reload;
        slowing = weapon.slowing;
        slowLength = weapon.slowLength;
        force = weapon.force;
        bleedDamage = weapon.bleedDamage;
        bleedTime = weapon.bleedTime;
        healPerShot = weapon.healPerShot;
    }

    /// <summary>
    /// updates the ui text to display the current ammo
    /// </summary>
    // Update is called once per frame
    void Update() {
        ammoDisplay.text = currentAmmo.ToString();
    }

    void OnAttack(InputValue value) {
        isPressed = value.isPressed;
        Fire();
    }

    /// <summary>
    /// when the attack trigger is pressed on the controller,
    /// the gun will fire as long as the gun ahs ammo, the player isnt in grace period, and the guns's script is enabled
    /// </summary>
    private void Fire() {
        if (readyToFire && currentAmmo > 0 && gameObject.GetComponent<PlayerHealth>().isGracePeriod == false && gameObject.GetComponent<BaseGun>().enabled){

            ParticleFx();

            playerAnim.SetBool("Attacking", true);

            RaycastHit hit;
            // creates a ray cast that checks for hits 
            Ray ray = new Ray(crosshair.transform.position, crosshair.transform.forward);

            if (Physics.Raycast(ray, out hit, range)) {
                GameObject target = hit.collider.gameObject;
                // if a player is hit, they take damage and the name of who shot them is passed onto their health script
                if (target.GetComponent<PlayerHealth>() != null) {
                    target.GetComponent<PlayerHealth>().TakeDamage(damage);
                    StartCoroutine(HitIndicator());
                    target.GetComponent<PlayerHealth>().killedBy = gameObject.name;
                    StartCoroutine(target.GetComponent<PlayerMovement>().SlowMovement(slowing, slowLength));
                    StartCoroutine(target.GetComponent<PlayerHealth>().Bleed(bleedDamage, bleedTime));
                    
                    gameObject.GetComponent<PlayerHealth>().HealDamage(healPerShot);
                }
            }

            currentAmmo--;

            if (weapon.name == "Recoil Shotgun") {
                StartCoroutine(gameObject.GetComponent<PlayerMovement>().KnockBack(new Vector2(0, -1), 1));
            }

            gameObject.GetComponent<Rumble>().RumbleConstant(0.5f, 0.5f, 0.3f);
            readyToFire = false;
            StartCoroutine(SemiAuto());
        }
    }

    // starts a count down before the next shot can be fired
    private IEnumerator SemiAuto(){
        yield return new WaitForSeconds(fireRate);
        readyToFire = true;

        if (isPressed && (weapon.name == "Glue Gun")) {
            Fire();
        }
    }

    // when the reload button is pressed, increase ammo to be maximum
    private IEnumerator OnReload() {
        // TODO: Play reload animation
        yield return new WaitForSeconds(reload);
        currentAmmo = ammo;
    }

    // plays the particle fx for the gun
    private void ParticleFx(){
        switch(weapon.name){
            case "Yee-Haw Gun":
                weapon.GetComponentInChildren<VisualEffect>().Play();
                break;

            case "Recoil Shotgun":
            weapon.GetComponentInChildren<ParticleSystem>().Play();
            break;

            case "Glue Gun":
            weapon.GetComponentInChildren<ParticleSystem>().Play();
            break;
            
            case "Nail Gun":
            weapon.GetComponentInChildren<ParticleSystem>().Play();
            break;

        }
    }

    // displays a hit marker to indicate that you have hit the player
    private IEnumerator HitIndicator(){
        hitInd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitInd.SetActive(false);
    }
}
