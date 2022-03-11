using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsOptions : MonoBehaviour {

    PlayerControls controls;

    private int menuSelection = 0;

    [SerializeField]
    private GameObject optionsMenu;

    /// <summary>
    /// upon the object being set active a new instance of the controls system is created
    /// when certain buttons are pressed, certain functions are called
    /// </summary>
    void Awake() {

        optionsMenu.GetComponent<OptionsScreen>().enabled = false;
        controls = new PlayerControls();

        //controls.Gameplay.Pause.performed += ctx => Pause();
        //controls.Gameplay.MenuDown.performed += ctx => DecrementOption();
        //controls.Gameplay.MenuUp.performed += ctx => IncrementOption();
        controls.Gameplay.Jump.performed += ctx => SelectOption();
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

    //private void DecrementOption() {
    //    if (menuSelection == 3) {
    //        menuSelection = 0;
    //    }
    //    else {
    //        menuSelection += 1;
    //    }
    //}

    //private void IncrementOption() {
    //    if (menuSelection == 0) {
    //        menuSelection = 3;
    //    }
    //    else {
    //        menuSelection -= 1;
    //    }

    //}

    /// <summary>
    /// when the 'a' button is pressed on the controller, 
    /// this checks the currently selected menu option and will  execute the right function
    /// </summary>
    private void SelectOption() {
        switch (menuSelection) {
            case 0: {
                    Back();
                    break;
                }
            //case 1: {

            //        break;
            //    }
            //case 2: {

            //        break;
            //    }
            //case 3: {

            //        break;
            //    }
            default: {
                    break;
                }
        }
    }

    /// <summary>
    /// when the back option is selected, the controls ui is set inactive and the options menu is activated
    /// </summary>
    private void Back() {

        optionsMenu.GetComponent<OptionsScreen>().enabled = true;
        optionsMenu.SetActive(true);
        gameObject.GetComponent<ControlsOptions>().enabled = false;
        gameObject.SetActive(false);
        
    }
}
