using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DungeonCreator : MonoBehaviour
{
    [SerializeField] private TextAsset dungeonText;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject NorthSouthCoridor;
    [SerializeField] private GameObject EastWestCoridor;

    private List<GenericDungeonRoom> rooms = new List<GenericDungeonRoom>();

    private void Awake()
    {
        DungeonRepresentation dungeon = (DungeonRepresentation)JsonUtility.FromJson(dungeonText.text, typeof(DungeonRepresentation));
        int length = dungeon.coordinates.Length;
        for (int i = 0; i < length; i++)
        {
            IntVector2 pos = dungeon.coordinates[i];
            Vector3 newPosition = new Vector3(pos.x, -0.05f, pos.z);
            GenericDungeonRoom room = Instantiate(prefab, newPosition, Quaternion.identity, transform).GetComponent<GenericDungeonRoom>();
            room.InitializeRoom(dungeon.doors[i].ToArray(), pos, i == 0 ? 0 : dungeon.room_difficulty[i]);
            rooms.Add(room);
        }
        //Find neighbours
        FindNeighbours();
        //connect rooms that are connected.

        //Remove unessesary doors and no door rooms
    }

    private void FindNeighbours()
    {
        GenericDungeonRoom neighbour;
        foreach (var item in rooms)
        {
            if (!item.hasbeenConnected[0])
            {
                neighbour = rooms.Find((room) => room.roomPosition.x == item.roomPosition.x - 30 && room.roomPosition.z == item.roomPosition.z);
                if (!neighbour.hasbeenConnected[2])
                {
                    item.AddUniqueNeighbour(neighbour);
                    neighbour.AddUniqueNeighbour(item);
                    item.hasbeenConnected[0] = true;
                    neighbour.hasbeenConnected[2] = true;
                    Vector3 coridorPosition = new Vector3((item.transform.position.x + neighbour.transform.position.x) / 2, item.transform.position.y, item.transform.position.z);
                    Instantiate(NorthSouthCoridor, coridorPosition, Quaternion.identity, transform);
                }
                else
                    item.CloseDoor(0);
            }
            if (!item.hasbeenConnected[1])
            {
                neighbour = rooms.Find((room) => room.roomPosition.x == item.roomPosition.x && room.roomPosition.z == item.roomPosition.z + 30);
                if (!neighbour.hasbeenConnected[3])
                {
                    item.AddUniqueNeighbour(neighbour);
                    neighbour.AddUniqueNeighbour(item);
                    item.hasbeenConnected[1] = true;
                    neighbour.hasbeenConnected[3] = true;
                    Vector3 coridorPosition = new Vector3(item.transform.position.x, item.transform.position.y, (item.transform.position.z + neighbour.transform.position.z) / 2);
                    Instantiate(EastWestCoridor, coridorPosition, Quaternion.identity, transform);
                }
                else
                    item.CloseDoor(1);
            }
            if (!item.hasbeenConnected[2])
            {
                neighbour = rooms.Find((room) => room.roomPosition.x == item.roomPosition.x + 30 && room.roomPosition.z == item.roomPosition.z);
                if (!neighbour.hasbeenConnected[0])
                {
                    item.AddUniqueNeighbour(neighbour);
                    neighbour.AddUniqueNeighbour(item);
                    item.hasbeenConnected[2] = true;
                    neighbour.hasbeenConnected[0] = true;
                    Vector3 coridorPosition = new Vector3((item.transform.position.x + neighbour.transform.position.x) / 2, item.transform.position.y, item.transform.position.z);
                    Instantiate(NorthSouthCoridor, coridorPosition, Quaternion.identity, transform);
                }
                else
                    item.CloseDoor(2);
            }
            if (!item.hasbeenConnected[3])
            {
                neighbour = rooms.Find((room) => room.roomPosition.x == item.roomPosition.x && room.roomPosition.z == item.roomPosition.z - 30);
                if (!neighbour.hasbeenConnected[1])
                {
                    item.AddUniqueNeighbour(neighbour);
                    neighbour.AddUniqueNeighbour(item);
                    item.hasbeenConnected[3] = true;
                    neighbour.hasbeenConnected[1] = true;
                    Vector3 coridorPosition = new Vector3(item.transform.position.x, item.transform.position.y, (item.transform.position.z + neighbour.transform.position.z) / 2);
                    Instantiate(EastWestCoridor, coridorPosition, Quaternion.identity, transform);
                }
                else
                    item.CloseDoor(3);
            }
        }
    }
}
