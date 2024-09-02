using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = .1f;

    [SerializeField]
    Collider fieldCollider;

    [SerializeField]
    private Transform player;

    private float xMin, yMin, xMax, yMax,
                  xVal, yVal;


    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        xMin = fieldCollider.bounds.min.x;
        yMin = fieldCollider.bounds.min.z;
        xMax = fieldCollider.bounds.max.x;
        yMax = fieldCollider.bounds.max.z;
    }
    private void Update()
    {
        transform.LookAt(player.position);
        yVal = transform.position.z + 1 * moveSpeed;
        yVal = Mathf.Clamp(yVal, yMin, yMax);

        transform.position = new Vector3(0, 0, yVal);

    }
}
