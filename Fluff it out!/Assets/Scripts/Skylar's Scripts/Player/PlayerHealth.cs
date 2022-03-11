using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the player's health
/// </summary>
public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private CharacterController playerController;
    private float maxHealth = 100f;
    private float currentHealth;
    public string killedBy;
    public Slider slider;
    public bool isGracePeriod = false;
    [SerializeField]
    private GameObject hitInd;
    [SerializeField]
    private GameObject fluffParticle;

    /// <summary>
    /// upon the player first being set active, the respawn function is called to reset all health and move them to their spawn point
    /// </summary>
    void Awake() {
        Respawn();
    }

    /// <summary>
    /// sets the health ui component to be full
    /// </summary>
    public void SetMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;
    }

    /// <summary>
    /// set the ui component of the health to be the current value of the player's health
    /// </summary>
    public void SetHealth(float health) {
        slider.value = health;
    }

    /// <summary>
    /// update the health bar
    /// </summary>
    // Update is called once per frame
    void Update() {
        SetHealth(currentHealth);
    }

    /// <summary>
    /// reduce the player's health
    /// </summary>
    /// <param name="value"> damage to be taken </param>
    public void TakeDamage(float value) {
        if (!isGracePeriod) {
            currentHealth -= value;
        }

        StartCoroutine(HitIndic());

        // if the player hits 0, then the killer is given a score of 1, and the player respawns
        if (currentHealth <= 0) {
            currentHealth = 0;
            GiveScore();
            Respawn();

        }
    }

    /// <summary>
    /// The player will take damage at 0.5 second intervals for a specified length of time
    /// </summary>
    /// <param name="damage"> amount of damage to be done each time they take damage </param>
    /// <param name="time"> length of time they will bleed for </param>
    public IEnumerator Bleed (float damage, float time) {
        float totalTime = 0;
        while (totalTime <= time) {
            yield return new WaitForSeconds(0.5f);
            TakeDamage(damage);
            totalTime += 0.5f;
        }

    }

    /// <summary>
    /// increase the player's health by a given amount
    /// </summary>
    /// <param name="value"> healing to be given </param>
    public void HealDamage(float value) {
        currentHealth += value;

        // make sure they cant go beyond their maximum
        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
    }

    /// <summary>
    /// get the object of the player that killed the player,
    /// access their score script and call the increment function so their score increases
    /// </summary>
    private void GiveScore() {
        GameObject killer = GameObject.Find(killedBy);
        if (killer != null) {
            killer.GetComponent<Scoring>().IncrementScore();
            killedBy = null;
        }

    }

    /// <summary>
    /// the player will be set to not be able to move so that they can be teleported to their spawn point and set back to move enabled
    /// the health is then set back to full and the grace period is started
    /// </summary>
    private void Respawn() {
        gameObject.GetComponent<Scoring>().ResetStreak();
        playerController.enabled = false;
        gameObject.transform.position = GameObject.Find(gameObject.name + " Respawn").transform.position; // the spawn point
        playerController.enabled = true;
        currentHealth = maxHealth;
        StartCoroutine(GracePeriod());
    }

    /// <summary>
    /// set as being in the grace period for 5 seconds after respawning
    /// </summary>
    private IEnumerator GracePeriod() {
        isGracePeriod = true;

        yield return new WaitForSeconds(5);

        isGracePeriod = false;
    }

    private IEnumerator HitIndic() {
        fluffParticle.GetComponent<ParticleSystem>().Play();
        hitInd.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitInd.SetActive(false);
    }
}
