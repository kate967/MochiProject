using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 sending;
    public GameObject player;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            player.transform.position = sending;
        }
    }
}
