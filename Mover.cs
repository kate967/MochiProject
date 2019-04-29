using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform movingPlatform;
    public Vector3 newPosition;
    public Vector3 oldPosition;
    public float smooth;

    private float distance;
    private bool reached = false;
    private Vector3 currPosition;

    void Awake()
    {
        currPosition = transform.position;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!reached)
        {
            distance = Vector3.Distance(transform.position, newPosition);

            if (distance > 0.1)
            {
                movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
            }
            else
            {
                reached = true;
            }
        }
        else
        {
            distance = Vector3.Distance(transform.position, oldPosition);

            if (distance > 0.1)
            {
                movingPlatform.position = Vector3.Lerp(movingPlatform.position, oldPosition, smooth * Time.deltaTime);
            }
            else
            {
                reached = false;
            }
        }
    }
}
