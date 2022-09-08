using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawTrajectory : MonoBehaviour
{
    private DiceLauncher _diceLauncher;
    private LineRenderer _lineRenderer;

    public int numPoints = 50;
    public float timeBetweenPoints = 0.1f;

    private void Awake()
    {
        _diceLauncher = GetComponent<DiceLauncher>();
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        _lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = _diceLauncher.transform.position;
        Vector3 startingVelocity = _diceLauncher.transform.up * _diceLauncher.Direction.y / 300f;
        for (float t = 0; t < numPoints; t+= timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            //newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            newPoint = startingPosition + startingVelocity * t + Physics.gravity / 2f * t * t;
            points.Add(newPoint);
        }
        _lineRenderer.SetPositions(points.ToArray());
    }
}
