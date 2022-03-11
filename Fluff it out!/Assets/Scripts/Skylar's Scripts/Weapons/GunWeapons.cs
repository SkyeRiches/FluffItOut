using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class assigns the values for each gun
/// </summary>
public class GunWeapons : MonoBehaviour {

    public float damage;
    public float range;
    public float fireRate;
    public int ammo;
    public float reload;
    public float slowing; // needs to be between 0 and 1 as its a multiplier to the enemies speed
    public float slowLength;
    public float force;
    public float bleedDamage;
    public float bleedTime;
    public float healPerShot;

    /// <summary>
    /// The types of guns there are
    /// </summary>
    public enum GunType {
        yeehaw,
        recoilShotgun,
        glueGun,
        nailGun,
        theCollector,
    }

    private void Start(){
        OnSpawn();
    }

    /// <summary>
    /// When the gun is spawned, it will be assigned the type of gun it is based on its name
    /// </summary>
    public void OnSpawn() {
        string name = gameObject.name;
        switch (name) {
            case "Yee-Haw Gun":
                SetWeaponType(GunType.yeehaw);
                break;

            case "Recoil Shotgun":
                SetWeaponType(GunType.recoilShotgun);
                break;

            case "Glue Gun":
                SetWeaponType(GunType.glueGun);
                break;

            case "Nail Gun":
                SetWeaponType(GunType.nailGun);
                break;

            case "The Collector":
                SetWeaponType(GunType.theCollector);
                break;

            default:
                break;
        }
            

    }

    /// <summary>
    /// This function assigns all the different values for the gun to the base gun script
    /// </summary>
    /// <param name="guntype"> the type of gun this object is </param>
    private void SetWeaponType(GunType guntype) {
        switch (guntype) {
            case GunType.yeehaw:
                damage = 20f;
                range = 40f;
                fireRate = 0.75f;
                ammo = 15;
                reload = 1f;
                slowing = 1f;
                slowLength = 0f;
                force = 0f;
                bleedDamage = 0f;
                bleedTime = 0f;
                healPerShot = 0f;
                break;

            case GunType.recoilShotgun:
                damage = 100f;
                range = 10f;
                fireRate = 1f;
                ammo = 2;
                reload = 1f;
                slowing = 1f;
                slowLength = 0f;
                force = 50f;
                bleedDamage = 0f;
                bleedTime = 0f;
                healPerShot = 0f;
                break;

            case GunType.glueGun:
                damage = 10f;
                range = 30f;
                fireRate = 0.25f;
                ammo = 25;
                reload = 1f;
                slowing = 0.5f;
                slowLength = 5f;
                force = 0f;
                bleedDamage = 0f;
                bleedTime = 0f;
                healPerShot = 0f;
                break;

            case GunType.nailGun:
                damage = 10f;
                range = 50f;
                fireRate = 0.75f;
                ammo = 10;
                reload = 1f;
                slowing = 1f;
                slowLength = 0f;
                force = 0f;
                bleedDamage = 2f;
                bleedTime = 2f;
                healPerShot = 0f;
                break;

            case GunType.theCollector:
                damage = 2f;
                range = 50f;
                fireRate = 0.05f;
                ammo = 200;
                healPerShot = 1f;
                break;

            default:
                break;
        }
    }
}
