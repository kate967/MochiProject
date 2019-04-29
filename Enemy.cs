using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float targetRange;
    public float allowedRange = 0;
    public float followSpeed;
    public RaycastHit Shot;
    private bool spotted;
    private GameObject enemyCollider;

    void Awake()
    {
        spotted = false;
    }

    void Update()
    {
        if (spotted == true)
        {
            transform.LookAt(player.transform);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
            {
                targetRange = Shot.distance;
                if (targetRange >= allowedRange)
                {
                    followSpeed = 0.07f;
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
                }
                else
                {
                    followSpeed = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            spotted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            spotted = false;
        }
    }
}
