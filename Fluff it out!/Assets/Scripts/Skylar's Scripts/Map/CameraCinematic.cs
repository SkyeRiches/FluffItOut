using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematic : MonoBehaviour {

    [SerializeField]
    private GameObject gameStart;

    [SerializeField]
    private Cinematic rail;

    private int currentSegment = 1;
    private float transition;
    private bool isCompleted;

    public PlayMode mode;
    public float speed = 2.5f;
    public bool isReversed;
    public bool isLooping;
    public bool isToAndFro;

    float timer, cooldownTimer;
    bool timerActive, coolDownActive, movementEnabled;

    /// <summary>
    /// Initialises the values for the variables
    /// </summary>
    void Start(){
        timer = 1f;
        cooldownTimer = 1f;
        timerActive = true;
        movementEnabled = true;
        coolDownActive = false;
    }

    
    private void Update() {
        if (!rail) {
            return;
        }
        if (!isCompleted) {
            Play(!isReversed);
        }

        if (isCompleted) {
            // once path is complete, the game start script is triggered and the camera is made inactive
            gameStart.GetComponent<GameStart>().enabled = true;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Slowly moves the camera along the set path and rotates it to face the way the nodes face
    /// i used this tutorial: https://www.youtube.com/watch?v=URqjHIz6pts&t=222s
    /// </summary>
    /// <param name="forward"></param>
    private void Play(bool forward = true) {

            float magnitude = (rail.nodes[currentSegment + 1].position - rail.nodes[currentSegment].position).magnitude;
            float cameraSpeed = (Time.deltaTime * 1 / magnitude) * speed;
            transition += (forward) ? cameraSpeed : -cameraSpeed;

            if (transition > 1) {
                transition = 0;
                currentSegment++;

                if (currentSegment == rail.nodes.Length - 1) {
                    if (isLooping) {
                        if (isToAndFro) {
                            transition = 1;
                            currentSegment = rail.nodes.Length - 2;
                            isReversed = !isReversed;
                        }
                        currentSegment = 1;
                    }
                    else {
                        isCompleted = true;
                        return;
                    }
                }
            }
            else if (transition < 0) {
                transition = 1;
                currentSegment--;

                if (currentSegment == -1) {

                    if (isLooping) {

                        if (isToAndFro) {
                            transition = 1;
                            currentSegment = 1;
                            isReversed = !isReversed;
                        }
                        currentSegment = rail.nodes.Length - 2;
                    }
                    else {
                        isCompleted = true;
                        return;
                    }
                }
            }

        transform.position = rail.PosOnRail(currentSegment, transition, mode);
        transform.rotation = rail.Orientation(currentSegment, transition);
    }
}
