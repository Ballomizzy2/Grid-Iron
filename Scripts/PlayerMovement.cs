using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = .2f;
    private CameraScript script;


    private bool move = true;
    private Rigidbody rb;
    GameManager manager;

    [SerializeField]
    public Collider fieldCollider;

    [SerializeField]
    private GameObject player;

    private float xMin, yMin, xMax, yMax,
                  xVal, yVal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void Start()
    {
        fieldCollider = GameObject.Find("Field").GetComponent<Collider>();
        xMin = fieldCollider.bounds.min.x;
        yMin = fieldCollider.bounds.min.z;
        xMax = fieldCollider.bounds.max.x;
        yMax = fieldCollider.bounds.max.z;

        manager = FindObjectOfType<GameManager>();
        script = FindObjectOfType<CameraScript>();
        script.ToggleFollowingPlayer(true);
    }
    private void Update()
    {
        if (manager != null && manager.gameState == GameManager.GameState.begin) 
        {
            
            xVal = transform.position.x - Input.GetAxis("Vertical") * moveSpeed;
            yVal = transform.position.z + Input.GetAxis("Horizontal") * moveSpeed;
            player.transform.eulerAngles += new Vector3(0, Input.GetAxis("Vertical"), 0);


            xVal = Mathf.Clamp(xVal, xMin, xMax);
            yVal = Mathf.Clamp(yVal, yMin, yMax);

            if (move)
            {
                transform.position =  new Vector3(xVal, 1, yVal);
            }

            float prod = Mathf.Sqrt(Mathf.Pow(xVal, 2) + Mathf.Pow(yVal, 2));
            script.Zoom(prod);

        
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TouchDown"))
        {
            manager.TouchDown();
            StartCoroutine(StopGame());
            script.ToggleFollowingPlayer(false);
            rb.AddForce(new Vector3(xVal, 0, yVal) * 0.5f, ForceMode.Impulse);
            move = false;
        }
    }

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1);
    }
}
