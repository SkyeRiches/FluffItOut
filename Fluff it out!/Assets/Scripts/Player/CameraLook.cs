using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour {

    public float lookSensitivity = 0.001f;

    [SerializeField]
    private Transform thePlayer;
    [SerializeField]
    private GameObject pivotPoint;
    [SerializeField]
    private Transform cameraPlayer;
    [SerializeField]
    private Transform lookTarget;

    private Vector2 lookRotation = new Vector2(0, 0);

    /// <summary>
    /// locks the cursor upon the game starting
    /// </summary>
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    /// <summary>
    /// the camera will roatate around a certain pivot point when looking up and down
    /// the whole player will rotate around when looking left and right
    /// </summary>
    void Update()
    {
        //cameraPlayer.eulerAngles.x < 45f && cameraPlayer.eulerAngles.x > -45f
        if (lookRotation != Vector2.zero){

            float cameraY = lookRotation.y * lookSensitivity * Time.deltaTime;
            float cameraX = lookRotation.x * lookSensitivity * Time.deltaTime;

            float xRotation = -cameraY;

            // This next bit will stop the camera going beyond certain values 
            // to emulate human perspective of not being able to look beyond certain angles
            if (xRotation > 0 && cameraPlayer.eulerAngles.x >= 45f && cameraPlayer.eulerAngles.x <= 314f) {
                xRotation = 0;
            }

            if (xRotation < 0 && cameraPlayer.eulerAngles.x <= 315f && cameraPlayer.eulerAngles.x >= 46f) {
                xRotation = 0;
            }

            //will rotate the camera up and down based on the previous bit
            //side to side looking will just rotate whole player object
            cameraPlayer.transform.RotateAround(pivotPoint.transform.position, pivotPoint.transform.right, xRotation);
            lookTarget.transform.RotateAround(pivotPoint.transform.position, pivotPoint.transform.right, xRotation);
            Mathf.Clamp(cameraPlayer.localEulerAngles.x, -45f, 45f);

            // if the camera goes out of bounds, it returns it back to be within the bounds
            // (this was to fix a bug where the camera got dislodged out of the bounds by an object)
            if (cameraPlayer.localEulerAngles.x > 45f && cameraPlayer.localEulerAngles.x < 180f) {
                float angle = cameraPlayer.eulerAngles.x - 45;
                cameraPlayer.transform.RotateAround(pivotPoint.transform.position, pivotPoint.transform.right, -angle);
                lookTarget.transform.RotateAround(pivotPoint.transform.position, pivotPoint.transform.right, -angle);
            }
            if (cameraPlayer.localEulerAngles.x < 315f && cameraPlayer.localEulerAngles.x > 180f) {
                float angle = cameraPlayer.eulerAngles.x + 45;
                cameraPlayer.transform.RotateAround(pivotPoint.transform.position, pivotPoint.transform.right, -angle);
                lookTarget.transform.RotateAround(pivotPoint.transform.position, pivotPoint.transform.right, -angle);
            }

            thePlayer.Rotate(Vector3.up * cameraX);            
        }
    }

    /// <summary>
    /// when the right stick is moved, the value is stored in a vector2
    /// </summary>
    void OnLook(InputValue value) {
        lookRotation = Vector2.zero;
        lookRotation = value.Get<Vector2>();
    }
}
