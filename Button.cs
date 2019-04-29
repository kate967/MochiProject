using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject target;
    public GameObject[] doors;
    public bool winButton;

    private Mover targetController;
    private bool push;

    void Awake()
    {
        push = false;
        winButton = false;

        GameObject targetControllerObject = target;
        targetController = targetControllerObject.GetComponent<Mover>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (push == true && Input.GetKey(KeyCode.O))
        {
            Debug.Log("O was pressed");
            targetController.smooth = 1.5f;
        }

        if(winButton == true && push == true)
        {
            if (Input.GetKey(KeyCode.O))
            {
                Debug.Log("O was pressed");

                for (int i = 0; i < doors.Length; i++)
                {
                    Debug.Log("Destorying Doors");
                    Destroy(doors[i]);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Push is true");
            push = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Push is false");
            push = false;
        }
    }
}
