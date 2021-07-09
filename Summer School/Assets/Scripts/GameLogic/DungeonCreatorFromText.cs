using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonCreatorFromText : MonoBehaviour
{
    //private string text = "[{'x': 0, 'y': 0, 'difficulty': 3, 'doors': [1, 1, 1, 1]}, {'x': -30, 'y': 0, 'difficulty': 7, 'doors': [0, 0, 1, 0]}, {'x': 30, 'y': 0, 'difficulty': 6, 'doors': [1, 0, 1, 0]}, {'x': 0, 'y': -30, 'difficulty': 7, 'doors': [0, 1, 0, 1]}, {'x': 0, 'y': 30, 'difficulty': 3, 'doors': [0, 1, 0, 1]}, {'x': 60, 'y': 0, 'difficulty': 1, 'doors': [1, 1, 0, 0]}, {'x': 0, 'y': -60, 'difficulty': 8, 'doors': [0, 1, 0, 0]}, {'x': 0, 'y': 60, 'difficulty': 8, 'doors': [0, 0, 0, 1]}, {'x': 60, 'y': 30, 'difficulty': 3, 'doors': [1, 0, 0, 1]}, {'x': 30, 'y': 30, 'difficulty': 4, 'doors': [0, 0, 1, 0]}]";
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject NorthSouthCoridor;
    [SerializeField] private GameObject EastWestCoridor;
    [SerializeField] private InputField input;
    private List<GenericDungeonRoom> rooms = new List<GenericDungeonRoom>();

    public void CreateNewDungeon()
    {
        string txt = "{ \"Items\":";
        txt += input.text;
        txt += "}";
        string outputString = txt.Replace("\'", "\"");
        //Debug.Log(outputString);


        DungeonRoomFromText[] dungeon = (DungeonRoomFromText[])JsonHelper.FromJson<DungeonRoomFromText>(outputString);
        Debug.Log(dungeon.Length);




        //DungeonRepresentation dungeon = (DungeonRepresentation)JsonUtility.FromJson(dungeonText.text, typeof(DungeonRepresentation));
        int length = dungeon.Length;
        for (int i = 0; i < length; i++)
        {
            IntVector2 pos = new IntVector2(dungeon[i].x, dungeon[i].y);
            Vector3 newPosition = new Vector3(pos.x, -0.05f, pos.z);
            GenericDungeonRoom room = Instantiate(prefab, newPosition, Quaternion.identity, transform).GetComponent<GenericDungeonRoom>();

            room.InitializeRoom(dungeon[i].DoorsToArray(), pos, i == 0 ? 0 : dungeon[i].difficulty);
            rooms.Add(room);
        }
        FindNeighbours();
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

[System.Serializable]
public class DungeonRepresentationFromText
{
    [SerializeField] public DungeonRoomFromText[] dungeonRooms;
}

[System.Serializable]
public class DungeonRoomFromText
{
    [SerializeField] public int x;
    [SerializeField] public int y;
    [SerializeField] public int difficulty;
    [SerializeField] public int[] doors;

    public bool[] DoorsToArray()
    {
        bool[] bools = new bool[4];
        bools[0] = doors[0] == 1 ? true : false;
        bools[1] = doors[1] == 1 ? true : false;
        bools[2] = doors[2] == 1 ? true : false;
        bools[3] = doors[3] == 1 ? true : false;
        return bools;
    }
}