using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private Transform[] _points;
    public Transform[] Points
    {
        get { return _points; }
    }
    private void OnDrawGizmosSelected()
    {
        int numOfPoints = _points.Length;
        for (int i = 0; i < numOfPoints; i++)
        {
            Vector3 point1 = _points[i].position;
            Vector3 point2 = _points[(i + 1) % numOfPoints].position;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(point1, point2);
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(point1, 0.5f);
        }
    }

}
