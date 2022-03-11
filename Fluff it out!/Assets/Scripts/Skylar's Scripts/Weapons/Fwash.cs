using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fwash : MonoBehaviour {

    [SerializeField]
    private GameObject fwashFX;

    private void OnEnable() {
        StartCoroutine(FXAppear());
    }

    /// <summary>
    /// Upon the script being enabled, the fwash effect will be dispalyed on the players screen for 5 seconds
    /// </summary>
    /// <returns></returns>
    private IEnumerator FXAppear() {
        fwashFX.SetActive(true);
        yield return new WaitForSeconds(5);
        fwashFX.SetActive(false);
        gameObject.GetComponent<Fwash>().enabled = false;
    }
}
