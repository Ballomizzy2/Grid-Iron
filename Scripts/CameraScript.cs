using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform player;
    float x, y, z;
    float xCush, zCush;
    int sign = -1;

    float zoomIn = 85, zoomOut = 130, zoomFactor;

    Camera cam;

    public bool follow = true, zoom = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        SetPlayer();
        xCush = Random.Range(0, 5);
        zCush = Random.Range(0, 5);
    }

    private void Update()
    {
        if(player != null)
        {
            transform.LookAt(player.position);            
        }
        else
            player = FindObjectOfType<PlayerMovement>().transform;
        if (Random.Range(0, 2) > 0.5f)
            sign = 1;
        else
            sign = -1;


        FollowPlayer();

        if (zoom)
        {
            float newVal = (10 * -zoomFactor) + 137;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newVal, Time.deltaTime/3);
        }
        
    }

    private void SetPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().transform;
        }
    }

    private void FollowPlayer()
    {
        if (!follow)
            return;
        x = player.position.x + 10;
        y = transform.position.y;
        z = player.position.z;
        transform.position = new Vector3 (x, y, z);
    }

    public void ToggleFollowingPlayer(bool foo)
    {
        follow = foo;
    }

    public void Zoom(float factor)
    {
        factor = Mathf.Abs(factor);
        factor %= 5;
        Debug.Log(factor);
        zoomFactor = factor;
        zoom = true;
        //cam.fieldOfView = (2 * -factor) + 137;
    }

}
