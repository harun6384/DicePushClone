using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] [Range(3,30)] private int lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();

    public static DrawTrajectory Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = flightDuration / lineSegmentCount;
        _linePoints.Clear();
        for (int i = 0; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 movementVector = new Vector3(
                velocity.x = stepTimePassed,
                velocity.y = stepTimePassed - .5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z = stepTimePassed
                );
            _linePoints.Add(movementVector + startingPoint);
        }
        lineRenderer.positionCount = _linePoints.Count;
        lineRenderer.SetPositions(_linePoints.ToArray());
    }
    public void HideLine()
    {
        lineRenderer.positionCount = 0;
    }

}
