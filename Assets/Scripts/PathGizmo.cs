using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGizmo : MonoBehaviour
{
    public Transform[] _wayPoints;

    private void OnDrawGizmosSelected()
    {
        int numOfPoints = _wayPoints.Length;
        for (int i = 0; i < numOfPoints; i++)
        {
            Vector3 point1 = _wayPoints[i].position;
            Vector3 point2 = _wayPoints[(i + 1) % numOfPoints].position;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(point1, point2);
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(point1, 0.5f);
        }
    }

}
