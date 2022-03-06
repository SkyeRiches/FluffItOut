using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used this tutorial https://www.youtube.com/watch?v=URqjHIz6pts&t=222s
/// </summary>
public class Cinematic : MonoBehaviour {

    public Transform[] nodes;

    private void Start() {
        nodes = GetComponentsInChildren<Transform>();
    }

    /// <summary>
    /// returns the current position on the rail
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="ratio"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public Vector3 PosOnRail(int segment, float ratio, PlayMode mode) {
        switch (mode) {
            case PlayMode.linear:
                return LinearPos(segment, ratio);

            case PlayMode.catmull:
                return CatmullPos(segment, ratio);

            default:
                return LinearPos(segment, ratio);
        }
    }

    /// <summary>
    /// returns position for the linear path when called
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public Vector3 LinearPos(int segment, float ratio) {
        Vector3 pos1 = nodes[segment].position;
        Vector3 pos2 = nodes[segment + 1].position;

        return Vector3.Lerp(pos1, pos2, ratio);
    }

    /// <summary>
    /// returns the orientation of the section on the path
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public Quaternion Orientation(int segment, float ratio) {
        Quaternion quat1 = nodes[segment].rotation;
        Quaternion quat2 = nodes[segment + 1].rotation;

        return Quaternion.Lerp(quat1, quat2, ratio);
    }

    /// <summary>
    /// Returns position on the path if using catmull curves
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public Vector3 CatmullPos(int segment, float ratio) {
        Vector3 p1, p2, p3, p4;

        if (segment == 1) {
            p1 = nodes[segment].position;
            p2 = p1;
            p3 = nodes[segment + 1].position;
            p4 = nodes[segment + 2].position;
        }
        else if (segment == nodes.Length - 2) {
            p1 = nodes[segment - 1].position;
            p2 = nodes[segment].position;
            p3 = nodes[segment + 1].position;
            p4 = p3;
        }
        else {
            p1 = nodes[segment - 1].position;
            p2 = nodes[segment].position;
            p3 = nodes[segment + 1].position;
            p4 = nodes[segment + 2].position;
        }

        float t2 = ratio * ratio;
        float t3 = t2 * ratio;

        // Equation for a catmull-Rom spline
        float x = 0.5f * ((2.0f * p2.x) + (-p1.x + p3.x) * ratio + (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) * t2 + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x) * t3);
        float y = 0.5f * ((2.0f * p2.y) + (-p1.y + p3.y) * ratio + (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y) * t2 + (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y) * t3);
        float z = 0.5f * ((2.0f * p2.z) + (-p1.z + p3.z) * ratio + (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z) * t2 + (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z) * t3);

        return new Vector3(x, y, z);
    }
}
