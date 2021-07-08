using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            other.GetComponent<RoomSpawner>().spawned = true;
            Destroy(other.gameObject);
        }
    }
}
