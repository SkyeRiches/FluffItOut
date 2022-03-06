using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will freeze time upon this script being enabled,
/// and resume time when the script is disabled
/// </summary>
public class TimeFreeze : MonoBehaviour {

    private void OnEnable() {
        Time.timeScale = 0f;
    }

    private void OnDisable() {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update() {
        Time.timeScale = 0f;
    }
}
