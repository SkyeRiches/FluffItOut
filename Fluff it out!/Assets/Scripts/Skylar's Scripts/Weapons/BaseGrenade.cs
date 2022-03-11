using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrenade : MonoBehaviour {

    [SerializeField]
    private bool slotFull;
    private Rigidbody rb;
    private Collider grenadeCol;
    private GameObject grenade;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject gunContainer;

    private float pickUpRange = 30f;
    private float throwForwardForce = 30f, throwUpwardForce = 15f;

    private bool nearGrenade;

    /// <summary>
    /// checks for nearby grenades, the closest one is saved to a variable incase the palyer presses the pickup
    /// </summary>
    private void Update() {
        if (slotFull) {
            grenade = gameObject.GetComponentInChildren<GrenadeFX>().gameObject;
            rb = grenade.GetComponent<Rigidbody>();
            grenadeCol = grenade.GetComponent<Collider>();
        }
        else {
            int count = 0;
            // Check if is within range and has pressed "E"
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Grenade")) {

                Vector3 distance = gameObject.transform.position - go.transform.position;

                if (distance.magnitude <= pickUpRange) {
                    grenade = go;
                    nearGrenade = true;
                    break;
                }

                count++;

                if (count >= GameObject.FindGameObjectsWithTag("Weapon").Length) {
                    grenade = null;
                    grenadeCol = null;
                    rb = null;
                    nearGrenade  = false;
                }
            }
        }    
    }

    /// <summary>
    /// if they dont already ahve a grenade, it equips it
    /// </summary>
    private void OnGrenade() {
        if (!slotFull && nearGrenade) {

            nearGrenade = false;
            slotFull = true;

            rb = grenade.GetComponent<Rigidbody>();
            grenadeCol = grenade.GetComponent<Collider>();

            // sets player as parent of the grenade so no one else can grab it
            grenade.transform.parent = gunContainer.transform;

            // Make weapon a child and move it to default position
            grenade.transform.localPosition = new Vector3(0, 0, 0);
            grenade.transform.localRotation = new Quaternion(0, 0, 0, 0);


            // Make the rigid body Kinematic and Boxcollider a trigger
            rb.isKinematic = true;
            grenadeCol.isTrigger = true;
        }

        else if (slotFull) {
            StartCoroutine(GrenadeThrow());
        }
    }

    /// <summary>
    /// If they have a grenade in hand, they throw it, the animation for throwing is played
    /// </summary>
    /// <returns></returns>
    private IEnumerator GrenadeThrow() {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("HoldBomb");

        slotFull = false;

        grenade.transform.parent = null;

        rb.isKinematic = false;

        // uses the palyers velocity as the starting velocity for the grenade
        rb.velocity = gameObject.GetComponent<CharacterController>().velocity;

        // adds a force to the grenade to propel it forwards
        rb.AddForce(transform.forward * throwForwardForce, ForceMode.Impulse);
        rb.AddForce(transform.up * throwUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        grenadeCol.isTrigger = false;

        // triggers the function for the fx of the grenade
        grenade.GetComponent<GrenadeFX>().GrenadeInit();

        grenade = null;
        grenadeCol = null;
        rb = null;
    }
}
