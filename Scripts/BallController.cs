using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    Collider bounds;

    private float xMin, yMin, xMax, yMax,
                  xVal, yVal;

    private void Start()
    {
        xMin = bounds.bounds.min.x;
        yMin = bounds.bounds.min.z;
        xMax = bounds.bounds.max.x;
        yMax = bounds.bounds.max.z;
    }

    private void Update()
    {
        xVal = transform.localPosition.x + Random.Range(-1, 1);
        yVal = transform.localPosition.z + Random.Range(-1, 1);
        xVal = Mathf.Clamp(xVal, xMin, xMax);
        yVal = Mathf.Clamp(yVal, yMin, yMax);
        transform.localPosition = new Vector3(xVal, bounds.bounds.center.y, yVal);
    }
}
