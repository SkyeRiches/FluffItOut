using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the slider for the sfx volume
/// </summary>
public class SFXSlider : MonoBehaviour {

    [SerializeField]
    private GameObject slider;

    PlayerControls controls;

    /// <summary>
    /// upon the object being set active, a new instance of the control scheme is created
    /// it then assigns the actions of pressing certain buttons to certain functions
    /// </summary>
    private void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.MenuRight.performed += ctx => IncreaseVolume();
        controls.Gameplay.MenuLeft.performed += ctx => DecreaseVolume();
    }

    /// <summary>
    /// on the script being enabled, the control scheme is enabled
    /// </summary>
    void OnEnable() {
        controls.Gameplay.Enable();
    }

    /// <summary>
    /// upon the script being disabled, the control scheme is disabled
    /// </summary>
    void OnDisable() {
        controls.Gameplay.Disable();
    }

    /// <summary>
    /// when the menu right button is pressed,the value of the sfx slider is increased by 1
    /// </summary>
    void IncreaseVolume() {
        slider.GetComponent<Slider>().value += 1;
    }

    /// <summary>
    /// when the menu left button is pressed, the value of the sfx slider is decreased by 1
    /// </summary>
    void DecreaseVolume() {
        slider.GetComponent<Slider>().value -= 1;
    }
}
