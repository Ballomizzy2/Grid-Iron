using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    private float moveSpeed;

    [SerializeField]
    Collider fieldCollider;

    [SerializeField]
    private Transform player;
    Rigidbody body;
    GameManager manager;

    private float xMin, yMin, xMax, yMax,
                  xVal, yVal;

    private float yConst;

    private bool move = true;


    private void Start()
    {
        fieldCollider = GameObject.Find("Field").GetComponent<Collider>();
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>().transform;
        body = GetComponent<Rigidbody>();
        fieldCollider = player.GetComponent<PlayerMovement>().fieldCollider;
        xMin = fieldCollider.bounds.min.x;
        yMin = fieldCollider.bounds.min.z;
        xMax = fieldCollider.bounds.max.x;
        yMax = fieldCollider.bounds.max.z;

        yConst = transform.position.y;

        moveSpeed = Random.Range(.1f, .15f);
    }
    private void Update()
    {
        if(manager != null && manager.gameState == GameManager.GameState.begin) 
        {
            transform.LookAt(player.position);
            yVal = moveSpeed;
            yVal = Mathf.Clamp(yVal, yMin, yMax -10);
            if (move)
                transform.position += (transform.TransformDirection(0, 0, yVal));
        }
        transform.position = new Vector3(transform.position.x, yConst, transform.position.z);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))// || col.gameObject.CompareTag("Defender"))
        {
            move = false;
            manager.Defended();
            //Time.timeScale = 0;
        }
        /*if (col.gameObject.CompareTag("Defender"))
        {
            move = false;
            //body.AddForceAtPosition(transform.right, transform.position);
        }*/
            //Bump();

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TouchDown"))
            move = false;
    }

    private void Bump() 
    {
            body.AddForceAtPosition(transform.right, transform.position);
    }
}
