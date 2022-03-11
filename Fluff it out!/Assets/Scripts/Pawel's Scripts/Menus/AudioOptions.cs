using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour {

    PlayerControls controls;

    private int menuSelection = 0;

    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject sfxHandle;
    [SerializeField]
    private GameObject masterHandle;
    [SerializeField]
    private GameObject musicHandle;
    [SerializeField]
    private GameObject backSelected;

    /// <summary>
    /// upon the object being set active a new instance of the controls system is created
    /// when certain buttons are pressed, certain functions are called
    /// </summary>
    void Awake() {

        optionsMenu.GetComponent<OptionsScreen>().enabled = false;
        controls = new PlayerControls();

        controls.Gameplay.MenuDown.performed += ctx => DecrementOption();
        controls.Gameplay.MenuUp.performed += ctx => IncrementOption();
    }

    /// <summary>
    /// when the script is enabled, so is the controls
    /// </summary>
    void OnEnable() {
        controls.Gameplay.Enable();
    }

    /// <summary>
    /// when the script is disabled, so is the controls
    /// </summary>
    void OnDisable() {
        controls.Gameplay.Disable();
    }

    /// <summary>
    /// when menu down button is pressed, decrease the menu selection by 1
    /// </summary>
    private void DecrementOption() {
        if (menuSelection == 3) {
            menuSelection = 0;
        }
        else {
            menuSelection += 1;
        }
    }

    /// <summary>
    /// when menu up button is pressed, increase the menu selection by 1
    /// </summary>
    private void IncrementOption() {
        if (menuSelection == 0) {
            menuSelection = 3;
        }
        else {
            menuSelection -= 1;
        }

    }

    /// <summary>
    /// sets the highlight on the currently selected option as active to give the user indication of which is selected
    /// </summary>
    void Update() {
        if (menuSelection == 0) {
            musicHandle.GetComponent<Image>().color = Color.cyan;
            sfxHandle.GetComponent<Image>().color = Color.white;
            masterHandle.GetComponent<Image>().color = Color.white;
            backSelected.SetActive(false);

            MusicVolume();
        }
        else if (menuSelection == 1) {
            musicHandle.GetComponent<Image>().color = Color.white;
            sfxHandle.GetComponent<Image>().color = Color.cyan;
            masterHandle.GetComponent<Image>().color = Color.white;
            backSelected.SetActive(false);

            MasterVolume();
        }
        else if (menuSelection == 2) {
            musicHandle.GetComponent<Image>().color = Color.white;
            sfxHandle.GetComponent<Image>().color = Color.white;
            masterHandle.GetComponent<Image>().color = Color.cyan;
            backSelected.SetActive(false);

            SFXVolume();
        }
        else if (menuSelection == 3) {
            backSelected.SetActive(true);
            musicHandle.GetComponent<Image>().color = Color.white;
            sfxHandle.GetComponent<Image>().color = Color.white;
            masterHandle.GetComponent<Image>().color = Color.white;

            controls.Gameplay.Jump.performed += ctx => Back();
        }
    }

    /// <summary>
    /// sets the slider script for music as active
    /// </summary>
    private void MusicVolume() {
        gameObject.GetComponent<MusicSlider>().enabled = true;
        gameObject.GetComponent<MasterSlider>().enabled = false;
        gameObject.GetComponent<SFXSlider>().enabled = false;
    }

    /// <summary>
    /// sets the slider script for master as active
    /// </summary>
    private void MasterVolume() {
        gameObject.GetComponent<MusicSlider>().enabled = false;
        gameObject.GetComponent<MasterSlider>().enabled = true;
        gameObject.GetComponent<SFXSlider>().enabled = false;
    }

    /// <summary>
    /// sets the slider script for sfx as active
    /// </summary>
    private void SFXVolume() {
        gameObject.GetComponent<MusicSlider>().enabled = false;
        gameObject.GetComponent<MasterSlider>().enabled = false;
        gameObject.GetComponent<SFXSlider>().enabled = true;
    }

    /// <summary>
    /// when the back option is selected, the audio ui is set inactive and the options menu is activated
    /// </summary>
    private void Back() {
        optionsMenu.GetComponent<OptionsScreen>().enabled = true;
        optionsMenu.SetActive(true);
        gameObject.GetComponent<AudioOptions>().enabled = false;
        gameObject.SetActive(false);

    }
}
