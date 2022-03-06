using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    #region Archive
    //[SerializeField]
    //private Rail rail;

    //private int currentSegment = 1;
    //private float transition;
    //private bool isCompleted;

    //public PlayMode mode;
    //public float speed = 2.5f;
    //public bool isReversed;
    //public bool isLooping;
    //public bool isToAndFro;

    //private void Update() {
    //    if (!rail) {
    //        return;
    //    }
    //    if (!isCompleted) {
    //        Play(!isReversed);
    //    }
    //}

    //private void Play(bool forward = true) {


    //    float magnitude = (rail.controlPoints[currentSegment + 1].position - rail.controlPoints[currentSegment].position).magnitude;
    //    float trainSpeed = (Time.deltaTime * 1 / magnitude) * speed;
    //    transition += (forward)? trainSpeed : -trainSpeed;

    //    if (transition > 1) {
    //        transition = 0;
    //        currentSegment++;

    //        if (currentSegment == rail.controlPoints.Length - 1) {
    //            if (isLooping) {
    //                if (isToAndFro) {
    //                    transition = 1;
    //                    currentSegment = rail.controlPoints.Length - 2;
    //                    isReversed = !isReversed;
    //                }
    //                currentSegment = 1;
    //            }
    //            else {
    //                isCompleted = true;
    //                return;
    //            }
    //        }
    //    }
    //    else if (transition < 0) {
    //        transition = 1;
    //        currentSegment--;

    //        if (currentSegment == - 1) {

    //            if (isLooping) {

    //                if (isToAndFro) {
    //                    transition = 1;
    //                    currentSegment = 1;
    //                    isReversed = !isReversed;
    //                }
    //                currentSegment = rail.controlPoints.Length - 2;
    //            }
    //            else {
    //                isCompleted = true;
    //                return;
    //            }
    //        }
    //    }

    //    //transform.position = rail.PosOnRail(currentSegment, transition, mode);
    //    //transform.rotation = rail.Orientation(currentSegment, transition);
    //    transform.rotation = rail.CurrentSegmentAngle(currentSegment);
    //}

    #endregion

    [SerializeField]
    public Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector3 trainPos;
    private float speedModifier;
    private bool coroutineReady;


    private Quaternion lookRotation;
    private Vector3 direction;

    /// <summary>
    /// Initialises the values
    /// </summary>
    private void Start() {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.75f;
        coroutineReady = true;

    }

    /// <summary>
    /// triggers the coroutine that makes the train move
    /// </summary>
    private void Update() {
        if (coroutineReady) {
            StartCoroutine(TravelOnRails(routeToGo));
        }        
    }

    /// <summary>
    /// the train will move along the created path rotating to face the right way and moving along
    /// i used the following tutorial: https://www.youtube.com/watch?v=11ofnLOE8pw
    /// </summary>
    /// <param name="routeNumber"></param>
    /// <returns></returns>
    private IEnumerator TravelOnRails(int routeNumber) {
        coroutineReady = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1) {
            tParam += Time.deltaTime * speedModifier;

            trainPos = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            direction = (trainPos - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);

            transform.position = trainPos;



            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo++;

        if (routeToGo > routes.Length - 1) {
            routeToGo = 0;
        }

        coroutineReady = true;
    }
}
