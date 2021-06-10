using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{

    public float viewAngle = 45f;
    public float viewDistance = 10f;
	public Vector3 fieldOrigin;

	public float fieldResolution = 0.5f;
	public float edgeDstThreshold = 0.5f;
	public int edgeAccuracy = 4;

    public LayerMask blockMask;


	public MeshFilter fieldMeshFilter;
	Mesh fieldMesh;

	private bool fieldContains;

	public UnityEvent onEnterField;
	public UnityEvent onExitField;
	

	/*
	 * Contains information about a raycast. 
	 * public bool hit: did it hit an obstacle.
	 * public Vector3 point: a vector of its' length and direction.
	 * public float len: the length of the ray.
	 * public float angle: the angle of the ray from start of field.
	 */
	public struct ViewCastInfo
	{
		public bool hit;
		public Vector3 point;
		public float len;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _len, float _angle)
		{
			hit = _hit;
			point = _point;
			len = _len;
			angle = _angle;
		}
	}

	/*
	 * Contains information for field edges calculations.
	 * public Vector3 pointA: where to add a starting cone.
	 * public Vector3 pointB: where to add an ending cone.
	 */
	public struct EdgeInfo
	{
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
		{
			pointA = _pointA;
			pointB = _pointB;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		fieldMesh = new Mesh();
		fieldMesh.name = "Field Mesh";
		fieldMeshFilter.mesh = fieldMesh;
	}

    // Update is called once per frame
    void Update()
    {
		if (InField())
		{
			if (!fieldContains)
			{
				onEnterField.Invoke();
				fieldContains = true;
			}
		}
		else if (fieldContains)
		{
			onExitField.Invoke();
			fieldContains = false;
		}
		DrawField();
    }

	/*
	 * Returns a vector representing angle.
	 */
	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	/*
	 * Returns information about a ray from given angle.
	 */
	ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, viewDistance, blockMask))
		{
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * viewDistance, viewDistance, globalAngle);
		}
	}

	/*
	 * Draws all the field of view meshes.
	 */
	void DrawField()
	{
		int stepCount = Mathf.RoundToInt(viewAngle * fieldResolution);
		float stepSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3>();
		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i <= stepCount; i++)
		{
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepSize * i;
			ViewCastInfo newViewCast = ViewCast(angle);

			if (i > 0)
			{
				bool edgeIsFar = Mathf.Abs(oldViewCast.len - newViewCast.len) > edgeDstThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeIsFar))
				{
					EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero)
					{
						viewPoints.Add(edge.pointA);
					}
					if (edge.pointB != Vector3.zero)
					{
						viewPoints.Add(edge.pointB);
					}
				}

			}


			viewPoints.Add(newViewCast.point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices[0] = fieldOrigin;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			vertices[i + 1] = transform.InverseTransformPoint(fieldOrigin + viewPoints[i]);

			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		fieldMesh.Clear();

		fieldMesh.vertices = vertices;
		fieldMesh.triangles = triangles;
		fieldMesh.RecalculateNormals();
	}


	EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
	{
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeAccuracy; i++)
		{
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast(angle);

			bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.len - newViewCast.len) > edgeDstThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
			{
				minAngle = angle;
				minPoint = newViewCast.point;
			}
			else
			{
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}

		return new EdgeInfo(minPoint, maxPoint);
	}

	// Retruns true if viewTarget (transform) is in the field of view
	public bool InField()
    {
		
		Vector3 targetPos = GameManager.Instance.PlayerTransform.position;
        if (Vector3.Distance(transform.position, targetPos) < viewDistance) // in distance
        {
            Vector3 targetDir = (targetPos - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, targetDir);
            if (angleToTarget < viewAngle / 2f) // in angle 
            {
                return !Physics.Linecast(transform.position, targetPos, blockMask); // isn't blocked
            }
        }
        return false;
    }
}
