using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] hasNorthDoor;
    public GameObject[] hasSouthDoor;
    public GameObject[] hasEastDoor;
    public GameObject[] hasWestDoor;

    public GameObject closedRoom;
    public List<GameObject> rooms;
    public DungeonRepresentation dungeon;

    private void Awake() => StartCoroutine(ExecuteAfter(0.75f));

    private IEnumerator ExecuteAfter(float secs)
    {
        while (secs >= 0)
        {
            yield return null;
            secs -= Time.deltaTime;
        }

        List<RoomRepresentation> roomRepresentations = new List<RoomRepresentation>();
        int length = rooms.Count;
        for (int i = 0; i < length; i++)
        {

            roomRepresentations.Add(new RoomRepresentation(rooms[i].GetComponent<RoomBehaviour>(), i));

        }

        dungeon = new DungeonRepresentation(roomRepresentations);
        //Debug.Log(JsonUtility.ToJson(dungeon));
    }
}
