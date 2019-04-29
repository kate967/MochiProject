using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject breakMe;
    private PlayerController playerController;

    private void Awake()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerController.canBreak == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(breakMe);
            }
        }
    }
}
