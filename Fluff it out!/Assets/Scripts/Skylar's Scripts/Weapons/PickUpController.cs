using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    public BaseGun gunScript;
    public BaseMelee meleeScript;
    public Rigidbody rb;
    public Collider gunColl;
    public Transform player, gunContainer, cam;

    private float pickUpRange = 5f;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public bool slotFull;
    private bool nearGun;

    private Vector3 distanceToPlayer;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private Text ammoCount;

    [SerializeField]
    private Animator playerAnim;

    /// <summary>
    /// when the player spawns the scripts and objects are set appropriately according to if a weapon is equipped
    /// </summary>
    // Start is called once at the start
    private void Start() {
        // Setup
        if (!equipped) {
            gunScript.enabled = false;
            meleeScript.enabled = false;
            slotFull = false;
        }

        if (equipped) {
            if (gameObject.GetComponentInChildren<MeleeFX>()) {
                meleeScript.enabled = true;
                gunScript.enabled = false;
                gun = gameObject.GetComponentInChildren<MeleeFX>().gameObject;
            }
            if (gameObject.GetComponentInChildren<GunWeapons>()) {
                gunScript.enabled = true;
                meleeScript.enabled = false;
                gun = gameObject.GetComponentInChildren<GunWeapons>().gameObject;
            }

            rb = gun.GetComponent<Rigidbody>();
            gunColl = gun.GetComponent<Collider>();

            rb.isKinematic = true;
            gunColl.isTrigger = true;
            slotFull = true;
        }
    }

    /// <summary>
    /// loop through all weapon objects in scene,
    /// if one is within pickup range break out the loop,
    /// if not then the loop will finish
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (equipped) {
            if (gameObject.GetComponentInChildren<MeleeFX>()) {
                gun = gameObject.GetComponentInChildren<MeleeFX>().gameObject;
            }
            if (gameObject.GetComponentInChildren<GunWeapons>()) {
                gun = gameObject.GetComponentInChildren<GunWeapons>().gameObject;
            }
            rb = gun.GetComponent<Rigidbody>();
            gunColl = gun.GetComponent<Collider>();
        }
        else {
            int count = 0;
            // Check if is within range and has pressed "E"
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Weapon")) {

                Vector3 distance = player.transform.position - go.transform.position;

                if (distance.magnitude <= pickUpRange) {
                    gun = go;
                    nearGun = true;
                    break;
                }

                count++;

                if (count >= GameObject.FindGameObjectsWithTag("Weapon").Length) {
                    gun = null;
                    gunColl = null;
                    rb = null;
                    nearGun = false;
                }
            }
        }

        if (!gameObject.GetComponent<BaseGun>().enabled){
            ammoCount.text = "00";
        }
    }

    /// <summary>
    /// when the pickup button is pressed, it will check if a weapon is in range and that there is a weapon slot available
    /// </summary>
    public void OnPickup() {
        if (!equipped && nearGun && !slotFull) {

            nearGun = false;
            equipped = true;
            slotFull = true;

            rb = gun.GetComponent<Rigidbody>();
            gunColl = gun.GetComponent<Collider>();

            gun.transform.parent = null;
            gun.transform.parent = gunContainer;

            // Make the rigid body Kinematic and Boxcollider a trigger
            rb.isKinematic = true;
            gunColl.isTrigger = true;

            if (gameObject.gameObject.GetComponentInChildren<GunWeapons>() != null){
                AssignGun();
            }
            if (gameObject.gameObject.GetComponentInChildren<MeleeFX>() != null){
                AssignMelee();
            }
            
        }
        else if (equipped) {
            // if a weapon is equipped, pressing pickup will throw your weapon
            Drop();
        }

    }

    /// <summary>
    /// the player will throw their gun and the gun will be detatched from the player object to be their own object
    /// </summary>
    public void Drop() {
        if (equipped) {
            equipped = false;
            slotFull = false;

            playerAnim.SetBool("Attacking", false);

            if (gameObject.gameObject.GetComponentInChildren<GunWeapons>() != null){
                UnAssignGun();
            }
            if (gameObject.gameObject.GetComponentInChildren<MeleeFX>() != null){
                UnAssignMelee();
            }

            // Set parent to null
            gun.transform.parent = null;

            // Make the rigid body not Kinematic and Boxcollider not a trigger
            rb.isKinematic = false;
            gunColl.isTrigger = false;

            // Gun carries momentum of player
            rb.velocity = player.GetComponent<CharacterController>().velocity;

            // Add force
            rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);

            // Add random rotation
            float random = Random.Range(-1f, 1f);
            rb.AddTorque(new Vector3(random, random, random) * 10);

            StartCoroutine(gun.GetComponent<Despawn>().WeaponDespawn());
            gun = null;
            gunColl = null;
            rb = null;
        }

    }

    /// <summary>
    /// Triggers the animations for holding the weapons and repositions them in the right place, also enables the correct scripts
    /// </summary>
    private void AssignGun(){
        string name = gameObject.GetComponentInChildren<GunWeapons>().gameObject.name;

        switch (name){

            case "Yee-Haw Gun":
                playerAnim.SetBool("HoldYee", true);

                gun.transform.localPosition = new Vector3(-0.248f, 0.543f, 0.042f);
                gun.transform.localEulerAngles = new Vector3(0, -90, -90);

                gunScript.enabled = true;
                gunScript.OnEquipped();
                meleeScript.enabled = false;
            break;

            case "Recoil Shotgun":
                playerAnim.SetBool("HoldShotGun", true);

                gun.transform.localPosition = new Vector3(-0.469f, 0.383f, -0.07f);
                gun.transform.localEulerAngles = new Vector3(25.436f, -47.016f, 131.639f);

                gunScript.enabled = true;
                gunScript.OnEquipped();
                meleeScript.enabled = false;
            break;

            case "Glue Gun":
                playerAnim.SetBool("HoldGlue", true);

                gun.transform.localPosition = new Vector3(-0.276f, 0.39f, 0.087f);
                gun.transform.localEulerAngles = new Vector3(-0.631f, -83.94701f, -180.011f);

                gunScript.enabled = true;
                gunScript.OnEquipped();
                meleeScript.enabled = false;
            break;

            case "Nail Gun":
                playerAnim.SetBool("HoldNail", true);

                gun.transform.localPosition = new Vector3(-0.044f, 0.26f, 0.103f);
                gun.transform.localEulerAngles = new Vector3(-5.67f, -84.075f, 91.46201f);

                gunScript.enabled = true;
                gunScript.OnEquipped();
                meleeScript.enabled = false;
            break;

            case "The Collector":
            gameObject.GetComponent<TheCollector>().enabled = true;
            gameObject.GetComponent<TheCollector>().Attack();

            playerAnim.SetBool("Collector", true);

            gun.transform.localPosition = new Vector3(-0.0258f, 0.723f, 0.005f);
            gun.transform.localEulerAngles = new Vector3(382.48f, -104.2f, 170.33f);

            
            meleeScript.enabled = false;
            gunScript.enabled = false;
            break;
        }
    }

    /// <summary>
    /// Triggers the animations for holding the weapons and repositions them in the right place, also enables the correct scripts
    /// </summary>
    private void AssignMelee(){
        string meleeName = gameObject.GetComponentInChildren<MeleeFX>().gameObject.name;

        switch(meleeName){
            //--------------------------------------------------------------------------------
            // Melee weapons
            //--------------------------------------------------------------------------------
            case "Lolipop Hammer":
                playerAnim.SetBool("HoldHammer", true);

                gun.transform.localPosition = new Vector3(0.91885f, 0.1399f, -0.0756f);
                gun.transform.localEulerAngles = new Vector3(178.282f, 100.22f, 175.242f);

                meleeScript.enabled = true;
                meleeScript.OnEquipped();
                gunScript.enabled = false;
            break;

            case "Spoon":
                playerAnim.SetBool("HoldHammer", true);

                gun.transform.localPosition = new Vector3(-1.03f, 0.03f, 0.12f);
                gun.transform.localEulerAngles = new Vector3(-0.207f, 7.099f, 91.659f);

                meleeScript.enabled = true;
                meleeScript.OnEquipped();
                gunScript.enabled = false;
            break;

            case "Squeaky Hammer":
                playerAnim.SetBool("HoldHammer", true);
                gun.transform.localPosition = new Vector3(-0.524f, 0.085f, 0.159f);
                gun.transform.localEulerAngles = new Vector3(180.631f, 96.05299f, 127.736f);

                meleeScript.enabled = true;
                meleeScript.OnEquipped();
                gunScript.enabled = false;
            break;

            case "Needle":
                playerAnim.SetBool("HoldHammer", true);

                gun.transform.localPosition = new Vector3(0.774f, 0.041f, -0.1f);
                gun.transform.localEulerAngles = new Vector3(-2.483f, -75.141f, 39.995f);

                meleeScript.enabled = true;
                meleeScript.OnEquipped();
                gunScript.enabled = false;
            break;

            case "Scissor":
                playerAnim.SetBool("HoldHammer", true);

                gun.transform.localPosition = new Vector3(-0.0669f, 0.1384f, 0.0820f);
                gun.transform.localEulerAngles = new Vector3(-75.349f, 179.615f, -77.588f);

                meleeScript.enabled = true;
                meleeScript.OnEquipped();
                gunScript.enabled = false;
            break;
        }
    }

    /// <summary>
    /// stops the animations for holding the weapons and disables the correct scripts
    /// </summary>
    private void UnAssignGun(){
        string name = gameObject.GetComponentInChildren<GunWeapons>().gameObject.name;

        gameObject.GetComponent<Rumble>().StopRumble();

        switch (name){

            case "Yee-Haw Gun":
            playerAnim.SetBool("HoldYee", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Recoil Shotgun":
            playerAnim.SetBool("HoldShotGun", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Glue Gun":
            playerAnim.SetBool("HoldGlue", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Nail Gun":
            playerAnim.SetBool("HoldNail", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "The Collector":
            playerAnim.SetBool("Collector", false);

            gameObject.GetComponent<TheCollector>().enabled = false;
            meleeScript.enabled = false;
            gunScript.enabled = false;
            break;
        }
    }

    /// <summary>
    /// stops the animations for holding the weapons and disables the correct scripts
    /// </summary>
    private void UnAssignMelee(){
        string meleeName = gameObject.GetComponentInChildren<MeleeFX>().gameObject.name;

        gameObject.GetComponent<Rumble>().StopRumble();

        switch(meleeName){

        case "Lolipop Hammer":
            playerAnim.SetBool("HoldHammer", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Spoon":
            playerAnim.SetBool("HoldHammer", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Squeaky Hammer":
            playerAnim.SetBool("HoldHammer", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Needle":
            playerAnim.SetBool("HoldHammer", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Scissor":
            playerAnim.SetBool("HoldHammer", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;

            case "Executioner":
            playerAnim.SetBool("HoldHammer", false);
            gunScript.enabled = false;
            meleeScript.enabled = false;
            break;
        }
    }

}
