using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need south door
    // 2 --> need west door
    // 3 --> need north door
    // 4 --> need east door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    private void Awake()
    {
        Destroy(gameObject, 4);
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.05f);
    }

    void Spawn()
    {
        if (!spawned)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.hasSouthDoor.Length);
                Instantiate(templates.hasSouthDoor[rand], transform.position, Quaternion.identity, templates.transform);
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.hasWestDoor.Length);
                Instantiate(templates.hasWestDoor[rand], transform.position, Quaternion.identity, templates.transform);
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.hasNorthDoor.Length);
                Instantiate(templates.hasNorthDoor[rand], transform.position, Quaternion.identity, templates.transform);
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.hasEastDoor.Length);
                Instantiate(templates.hasEastDoor[rand], transform.position, Quaternion.identity, templates.transform);
            }
            else if (openingDirection == 0)
            {
                Debug.Log(name);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity, templates.transform);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
