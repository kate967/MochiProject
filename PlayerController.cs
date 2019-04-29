using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded;
    public bool hasFollower;
    private float verticalVelocity;
    private float gravity = 14.0f;
    public float movementSpeed;

    public bool canBreak;
    public bool canDoubleJump;
    public int health;
    public int score;
    public bool takeIce;

    public GameObject follower;
    public GameObject player;
    public GameObject enemy;
    public List<FollowerController> followersController = new List<FollowerController>();
    private Enemy enemyController;
    private Button buttonController;

    public float jumpForce;
    public float speed;

    public Vector3 groundedOffset;
    public float groundedRadius;

    public Transform GameCamera;

    void Awake()
    {
        GameObject enemyControllerObject = GameObject.FindWithTag("Enemy");
        enemyController = enemyControllerObject.GetComponent<Enemy>();

        GameObject buttonControllerObject = GameObject.FindWithTag("WinButton");
        buttonController = buttonControllerObject.GetComponent<Button>();

        canBreak = false;
        canDoubleJump = false;

        health = 3;
        score = 0;
        takeIce = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    void Update()
    {
        var CharacterRotation = GameCamera.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;

        transform.rotation = CharacterRotation;

        CheckGrounded();

        if (isGrounded == true)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                rb.AddForce(new Vector3(0, verticalVelocity, 0));
            }
        }

        if(isGrounded == false && canDoubleJump == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                rb.AddForce(new Vector3(0, verticalVelocity, 0));
            }
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if (health >= 4)
        {
            canBreak = true;
        }
        else
        {
            canBreak = false;
        }

        if(health >= 5)
        {
            canDoubleJump = true;
        }
        else
        {
            canDoubleJump = false;
        }

        if(health <= 0)
        {
            Destroy(player);
        }
    }

    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        Debug.Log("Pre: " + movement);
        movement = transform.TransformDirection(movement);
        Debug.Log("Post: " + movement);
        movement = movement.normalized;
        movement = movement * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    void CheckGrounded()
    {
        isGrounded = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (groundedOffset * transform.lossyScale.x), groundedRadius * transform.lossyScale.x);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(hitColliders[i].name);
            if (hitColliders[i].tag == "Ground" || hitColliders[i].tag == "Elevator")
            {
                isGrounded = true;
                Debug.Log("Grounded: " + isGrounded);
                return;
            }
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            player.transform.localScale /= 1.25f;
            health--;
            Destroy(other.gameObject);
            Debug.Log("Health is: " + health);
        }
    }

    void OnCollisionExit(Collision other)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("IceCube") && hasFollower == true)
        {
            for(int i = 0; i < followersController.Count; i++)
            {
                followersController[i].SaveFollower(other.gameObject);
            }

            score++;
            Debug.Log("Score: " + score);
        }

        if(other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Elevator"))
        {
            isGrounded = true;
            transform.parent = other.transform;
        }

        if(other.gameObject.CompareTag("WinButton"))
        {
            buttonController.winButton = true;
        }

        if(other.gameObject.CompareTag("TakeRemainingIce"))
        {
            takeIce = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Elevator"))
        {
            transform.parent = null;
        }

        if (other.gameObject.CompareTag("WinButton"))
        {
            buttonController.winButton = false;
        }

        if (other.gameObject.CompareTag("TakeRemainingIce"))
        {
            takeIce = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (groundedOffset * transform.lossyScale.x), groundedRadius * transform.lossyScale.x);
    }
}
