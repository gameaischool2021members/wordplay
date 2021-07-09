using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotBasic : MonoBehaviour
{
    
    private GameObject target;
    private bool isSeeking;

    // Start is called before the first frame update

    private void Start()
    {
        
        target = gameObject;

    }

    

    private void Update()
    {
        if (isSeeking)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            // print("Distance to target: " + dist);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.006f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playableChar"))
         {
            isSeeking = true;
            target = other.gameObject;
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("playableChar"))
        {
            isSeeking = false;
            target = null;
        }
    }
}
