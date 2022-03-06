using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFX : MonoBehaviour {

    private float aoeRange;
    private float damage;
    private float boomDelay;
    private float pullForce;

    private bool gravityWellActive = false;

    [SerializeField]
    private GameObject particleFx;

    /// <summary>
    /// Intiialises the variables for the grenades and triggers the special functions of each grenade
    /// </summary>
    public void GrenadeInit() {
        string name = gameObject.name;

        gameObject.GetComponent<Collider>().material = Resources.Load("Materials/grenade") as PhysicMaterial;

        switch (name) {
            case "Fwash Bang":
                // plays distorted sound
                // displays white and some text on all player screen in range
                boomDelay = 2f;
                aoeRange = 100f;
                damage = 5f;
                StartCoroutine(Explosion(boomDelay));
                StartCoroutine(FwashBang(boomDelay));
                break;

            case "Dynamite":
                boomDelay = 3f;
                aoeRange = 10f;
                damage = 50f;
                StartCoroutine(Explosion(boomDelay));
                break;

            case "Sonic Boom":
                // Knocks back all players
                boomDelay = 5f;
                aoeRange = 50f;
                damage = 20f;
                StartCoroutine(Explosion(boomDelay));
                StartCoroutine(SonicBoom(boomDelay));
                break;

            case "Glitter Mine":
                // Smoke bomb that plays a curious sound
                boomDelay = 1.5f;
                aoeRange = 100f;
                damage = 15f;
                StartCoroutine(Explosion(boomDelay));
                StartCoroutine(GlitterMine(boomDelay));
                break;

            case "Jack In The Box":
                // random chance to be any of the grenades, as well as chance of decoy
                boomDelay = 3f;
                aoeRange = 100f;
                damage = 5f;
                StartCoroutine(Explosion(boomDelay));
                StartCoroutine(JackInBox(boomDelay));
                break;

            case "The Gravity Well":
                // Pulls all objects in close, dealing damage over time for 5 seconds
                boomDelay = 5f;
                aoeRange = 50f;
                damage = 5f;
                StartCoroutine(Explosion(boomDelay));
                StartCoroutine(TheGravityWell(boomDelay));

                break;

            default:
                break;
        }
    }

    /// <summary>
    /// for players in radius of the explosiev, they will take damage
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator Explosion(float delay) {
        yield return new WaitForSeconds(delay);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {

            Vector3 distance = gameObject.transform.position - go.transform.position;

            if (distance.magnitude <= aoeRange) {
                go.GetComponent<PlayerHealth>().TakeDamage(damage);

            }
        }

        // as this is all the dynamite does, it then destroys itself after with the particle effect
        if (gameObject.name == "Dynamite") {
            Instantiate(particleFx, transform.position, Quaternion.Euler(0,0,0));
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// makes the fwash effect appreas on screens ofd players nearby
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator FwashBang(float delay) {
        yield return new WaitForSeconds(delay);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {

            Vector3 distance = gameObject.transform.position - go.transform.position;

            if (distance.magnitude <= aoeRange) {
                go.GetComponentInChildren<Fwash>().enabled = true;
            }
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// pushes players away that are in the radius of it
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator SonicBoom(float delay) {
        yield return new WaitForSeconds(delay);

        particleFx.GetComponent<ParticleSystem>().Play();

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, aoeRange);
        List<GameObject> gos = new List<GameObject>();

        foreach (Collider collider in colliders) {
            if (collider.gameObject.GetComponent<Rigidbody>() != null && collider.name != name ||
                collider.gameObject.GetComponent<CharacterController>() != null && collider.name != name) {
                gos.Add(collider.gameObject);
            }
        }

        foreach (GameObject go in gos) {
            if (go.GetComponent<Rigidbody>() != null) {
                go.GetComponent<Rigidbody>().AddExplosionForce(10f, gameObject.transform.position, aoeRange, 5f, ForceMode.Impulse);
            }
            else if (go.GetComponent<CharacterController>() != null) {
                Vector3 distance = gameObject.transform.position - go.transform.position;

                Debug.Log(distance);

                float xKnockback;
                float zKnockback;

                if (distance.x >= 0) {
                    xKnockback = -1;
                }
                else {
                    xKnockback = 1;
                }

                if (distance.z >= 0) {
                    zKnockback = -1;
                }
                else {
                    zKnockback = 1;
                }

                StartCoroutine(go.GetComponent<PlayerMovement>().KnockBack(new Vector2(xKnockback, zKnockback), 2));
            }
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Displays a particle effect designed to restrict vision
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator GlitterMine(float delay) {
        yield return new WaitForSeconds(delay);

        GameObject glitter = Resources.Load("Prefabs/Glitter") as GameObject;

        Instantiate(glitter, gameObject.transform.position, new Quaternion(0, 0, 0, 0));

        Destroy(gameObject);
    }

    /// <summary>
    /// has a random chance to have same effect as another bomb or just be a dud that does nothing
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator JackInBox(float delay) {
        yield return new WaitForSeconds(delay);

        int random = Random.Range(0, 99);

        if (random < 15) {
            // play dud animation
            Destroy(gameObject);
        }
        else if (random >= 15 && random < 20) {
            StartCoroutine(TheGravityWell(0));
        }
        else if (random >= 20 && random < 36) {
            StartCoroutine(GlitterMine(0));
        }
        else if (random >= 36 && random < 52) {
            StartCoroutine(SonicBoom(0));
        }
        else if (random >= 52 && random < 68) {
            damage = 50;
            StartCoroutine(Explosion(0));
        }
        else if (random >= 68 && random < 84) {
            
        }
        else if (random >= 84 && random < 100) {
            StartCoroutine(FwashBang(0));
        }

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    /// <summary>
    /// Pulls enemies in within a large radius, and slowly damagews them over time
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator TheGravityWell(float delay) {
        yield return new WaitForSeconds(delay);

        gravityWellActive = true;
        GameObject blackHole = Instantiate(particleFx, transform.position, new Quaternion(0, 0, 0, 0));
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, aoeRange);

        foreach (Collider hit in colliders) {
            if (hit.tag == "Player") {
                Debug.Log(hit.tag);
                StartCoroutine(hit.gameObject.GetComponent<PlayerHealth>().Bleed(5f, 5f));
            }
        }

        yield return new WaitForSeconds(7);
        Destroy(blackHole);
        Destroy(gameObject);
    }

    /// <summary>
    /// Only used for void bomb, it constantly pulls players in
    /// </summary>
    private void Update() {

        if (gravityWellActive) {

            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, aoeRange);

            Debug.Log(colliders.Length);

            foreach (Collider hit in colliders) {

                Vector3 difference = hit.transform.position - transform.position;

                if (difference.magnitude > 5) {
                    pullForce = 2;
                }
                else {
                    pullForce = 100;
                }

                if (hit.gameObject.GetComponent<Rigidbody>()) {
                   hit.GetComponent<Rigidbody>().AddForce(difference.normalized * -pullForce * Time.deltaTime, ForceMode.Force);
                }

                if (hit.GetComponent<CharacterController>() != null) {
                    hit.GetComponent<CharacterController>().Move(-difference * pullForce * Time.deltaTime);
                }
            }
        }

    }
}
