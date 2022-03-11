using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RumblePattern {
    Constant,
    Pulse,
    Linear
}

/// <summary>
/// This class will make the controller vibrate when called
/// </summary>
public class Rumble : MonoBehaviour {
    private PlayerInput _playerInput;
    private RumblePattern activeRumblePattern;
    private float rumbleDuration;               // Duration is in seconds
    private float pulseDuration;
    private float lowFreqA;
    private float lowStep;
    private float highFreqA;
    private float highStep;
    private float rumbleStep;
    private bool isMotorActive = false;

    // Public Methods
    /// <summary>
    /// this function will make the controller vibrate constantly for the given length of time
    /// good for a quick vibration from something like firing a weapon
    /// </summary>
    /// <param name="low"> left of the controller (lower freq motor) </param>
    /// <param name="high"> right of the controller (higher freq motor) </param>
    /// <param name="duration"> length of time it should vibrate for </param>
    public void RumbleConstant(float low, float high, float duration) {
        activeRumblePattern = RumblePattern.Constant;
        lowFreqA = low;
        highFreqA = high;
        rumbleDuration = Time.time + duration;
    }

    /// <summary>
    /// this function will make the controller vibrate on a pattern of bursts for a certain amount of time
    /// good for damage over time
    /// </summary>
    /// <param name="low"> lower freq motor </param>
    /// <param name="high"> higher freq motor </param>
    /// <param name="burstTime"> length of each burst </param>
    /// <param name="duration"> length of time it should pulse vibrate for </param>
    public void RumblePulse(float low, float high, float burstTime, float duration) {
        activeRumblePattern = RumblePattern.Pulse;
        lowFreqA = low;
        highFreqA = high;
        rumbleStep = burstTime;
        pulseDuration = Time.time + burstTime;
        rumbleDuration = Time.time + duration;
        isMotorActive = true;
        Gamepad g = GetGamepad();
        g?.SetMotorSpeeds(lowFreqA, highFreqA);
    }

    /// <summary>
    /// This function makes a vibration in the controller that starts at one frequency and changes over time to the next frequency specified
    /// it will do this over a period of time specified
    /// </summary>
    /// <param name="lowStart"> lower freq motor starting freq </param>
    /// <param name="lowEnd"> lower freq motor end freq </param>
    /// <param name="highStart"> higher freq motor start freq </param>
    /// <param name="highEnd"> higher freq motor end freq </param>
    /// <param name="duration"> length of time it will vibrate for </param>
    public void RumbleLinear(float lowStart, float lowEnd, float highStart, float highEnd, float duration) {
        activeRumblePattern = RumblePattern.Linear;
        lowFreqA = lowStart;
        highFreqA = highStart;
        lowStep = (lowEnd - lowStart) / duration;
        highStep = (highEnd - highEnd) / duration;
        rumbleDuration = Time.time + duration;
    }

    /// <summary>
    /// This function stops the controller's vibration
    /// </summary>
    public void StopRumble() {
        Gamepad gamepad = GetGamepad();
        if (gamepad != null) {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }

    // Private Methods
    /// <summary>
    /// Assigns the player input component to a variable upon the object being set active
    /// </summary>
    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// This will make the controller vibrate according to the currently active vibration choice
    /// </summary>
    private void Update() {
        // if the time its vibrating for is longer than specified duration it stops the vibration
        if (Time.time > rumbleDuration) {
            StopRumble();
            return;
        }

        Gamepad gamepad = GetGamepad();

        if (gamepad == null) {
            return;
        }

        switch (activeRumblePattern) {
            // if the current active pattern is constant then it will just set a normal vibration for the controller
            case RumblePattern.Constant:
                gamepad.SetMotorSpeeds(lowFreqA, highFreqA);
                break;

            // if the pattern is pulse then it will turn vibration on and off for duration of the pulse
            case RumblePattern.Pulse:
                if (Time.time > pulseDuration) {
                    isMotorActive = !isMotorActive;
                    pulseDuration = Time.time + rumbleStep;

                    if (!isMotorActive) {
                        gamepad.SetMotorSpeeds(0, 0);
                    }
                    else {
                        gamepad.SetMotorSpeeds(lowFreqA, highFreqA);
                    }
                }
                break;
            
            // if pattern is linear it will set the vibration and increment the frequencies each time update loops though
            case RumblePattern.Linear:
                gamepad.SetMotorSpeeds(lowFreqA, highFreqA);
                lowFreqA += lowStep * Time.deltaTime;
                highFreqA += highStep * Time.deltaTime;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// This function will return the game pad that controls the player this script is attatched to
    /// </summary>
    private Gamepad GetGamepad() {
        Gamepad gamepad = null;
        foreach (Gamepad g in Gamepad.all) {
            foreach (var d in _playerInput.devices) {
                if (d.deviceId == g.deviceId) {
                    gamepad = g;
                    break;
                }
            }
            if (gamepad != null) {
                break;
            }
        }
        return gamepad;
    }
}
