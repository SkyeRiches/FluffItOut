using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPad : MonoBehaviour {

    private float coolDown = 5f;
    private Object[] weapons;
    private bool collected = true;
    private Vector3 spawnPos;

    // upon starting the game, the pads will get the folder the weapons are in and triggers the weapon spawn function
    private void Start() {
        weapons = Resources.LoadAll("Prefabs/Weapons", typeof(GameObject));
        spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);

        StartCoroutine(WeaponSpawn());
    }

    // if there is not already a weapon on the pad it will spawn a weapon
    private IEnumerator WeaponSpawn() {

        yield return new WaitForSeconds(coolDown);

        if (collected) {
            collected = false;

            int random = Random.Range(0, weapons.Length - 1);

            GameObject spawnedWeapon = Instantiate(weapons[random], spawnPos, gameObject.transform.rotation) as GameObject;

            spawnedWeapon.name = weapons[random].name;

            spawnedWeapon.GetComponent<Rigidbody>().isKinematic = true;

            spawnedWeapon.transform.parent = gameObject.transform;
        }

        StartCoroutine(WeaponSpawn());
    }

    // if their is a child to the pad, it means a weapon has not been collected
    private void Update() {
        if (gameObject.transform.childCount > 0) {
            collected = false;
        }
        else if (transform.childCount <= 0) {
            collected = true;
        }
    }

}
