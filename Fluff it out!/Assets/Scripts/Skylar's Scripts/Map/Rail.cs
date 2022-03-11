using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum PlayMode {
    linear,
    catmull,
}

public class Rail : MonoBehaviour {

    public Transform[] controlPoints;

    private Vector3 gizmosPosition;

    /// <summary>
    /// Gets the angle of turn on the current segment of the rail
    /// </summary>
    /// <param name="segment"></param>
    /// <returns></returns>
    public Quaternion CurrentSegmentAngle(int segment) {
        return (controlPoints[segment].rotation);
    }

    /// <summary>
    /// Creates a visible line of nodes to display the path of the rail
    /// </summary>
    private void OnDrawGizmos() {

        for (float t = 0; t <= 1; t += 0.05f) {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, controlPoints[0].position.z), new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, controlPoints[1].position.z));
        Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, controlPoints[2].position.z), new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, controlPoints[3].position.z));
    }
}
