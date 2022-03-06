using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {

    /// <summary>
    /// After 5 seconds the weapon will despawn (that is only if it has been dropped by a player)
    /// </summary>
    /// <returns></returns>
    public IEnumerator WeaponDespawn() {
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
