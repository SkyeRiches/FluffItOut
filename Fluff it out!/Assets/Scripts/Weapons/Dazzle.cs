using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dazzle : MonoBehaviour {

    [SerializeField]
    private GameObject dazzleFX;

    private void OnEnable() {
        StartCoroutine(FXAppear());
    }

    /// <summary>
    /// Upon the script being activated, the sazzle effect on the player will be triggered
    /// </summary>
    /// <returns></returns>
    private IEnumerator FXAppear() {
        dazzleFX.SetActive(true);
        yield return new WaitForSeconds(2);
        dazzleFX.SetActive(false);
        gameObject.GetComponent<Dazzle>().enabled = false;
    }
}
