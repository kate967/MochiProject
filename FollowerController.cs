using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    public GameObject player;
    public GameObject follower;
    private float bigger;
    private bool inFollowerSpace;
    private float targetRange;
    private float allowedRange = 5;
    private float followSpeed;

    private bool save;
    private RaycastHit Shot;

    private PlayerController playerController;

    private void Awake()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>();

        bigger = 1.25f;
    }

    void Update()
    {
        transform.LookAt(player.transform);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot) && save == true)
        {
            targetRange = Shot.distance;
            if (targetRange >= allowedRange)
            {
                followSpeed = 0.05f;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
            }
            else
            {
                followSpeed = 0;
            }
        }

        if(playerController.hasFollower == true)
        {
            allowedRange = 1.5f;
        }

        if (inFollowerSpace == true && Input.GetKey(KeyCode.Z) && playerController.followersController.Count < 1)
        {
            save = true;
            playerController.followersController.Add(this);
            playerController.hasFollower = true;
        }

        if (inFollowerSpace == true && playerController.hasFollower == true && Input.GetKey(KeyCode.P))
        {
            save = false;
            playerController.followersController.Remove(this);
            playerController.hasFollower = false;
        }

        if (inFollowerSpace == true && Input.GetKey(KeyCode.X))
        {
            Debug.Log("Health is: " + playerController.health);
            save = false;
            playerController.health++;
            AbsorbFollower();
        }

        if(playerController.health >= 5)
        {
            bigger = 1.0f;
        }

        if(playerController.health < 5)
        {
            bigger = 1.25f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inFollowerSpace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inFollowerSpace = false;
        }
    }

    public void AbsorbFollower()
    {
        playerController.followersController.Remove(this);
        playerController.hasFollower = false;
        player.transform.localScale *= bigger;
        Destroy(follower);
    }

    public void SaveFollower(GameObject obj)
    {
        playerController.hasFollower = false;
        playerController.followersController.Remove(this);
        player = obj;
        allowedRange = 1.0f;
    }
}
