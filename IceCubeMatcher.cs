using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubeMatcher : MonoBehaviour
{
    private Collider myColl;
    public Collider coll;

    void Awake()
    {
        myColl = coll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Follower"))
        {
            Destroy(myColl);
        }
    }
}
