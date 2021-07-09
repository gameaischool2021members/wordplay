using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class DungeonRepresentation
{
    [SerializeField] public int num_rooms;
    [SerializeField] public float average_difficulty;
    [SerializeField] public int easy_rooms;
    [SerializeField] public int normal_rooms;
    [SerializeField] public int hard_rooms;
    [SerializeField] public int[] room_difficulty;
    [SerializeField] public IntVector2[] coordinates;
    [SerializeField] public Doors[] doors;
    [SerializeField] public int[] design;

    public DungeonRepresentation(List<RoomRepresentation> roomRepresentations)
    {
        num_rooms = roomRepresentations.Count;
        room_difficulty = new int[num_rooms];
        coordinates = new IntVector2[num_rooms];
        doors = new Doors[num_rooms];
        design = new int[num_rooms];


        for (int i = 0; i < num_rooms; i++)
        {
            average_difficulty += roomRepresentations[i].difficulty;
            if (roomRepresentations[i].difficulty <= 4)
                easy_rooms++;
            else if (roomRepresentations[i].difficulty < 8)
                normal_rooms++;
            else
                hard_rooms++;

            room_difficulty[i] = roomRepresentations[i].difficulty;
            coordinates[i] = new IntVector2(roomRepresentations[i].center);
            doors[i] = new Doors(roomRepresentations[i].doors);
            design[i] = (int)roomRepresentations[i].design;
        }

        average_difficulty = average_difficulty / num_rooms;
    }

}


[System.Serializable]
public class IntVector2
{
    [SerializeField] public int x;
    [SerializeField] public int z;
    public IntVector2(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public IntVector2(Vector3 vector3) : this((int)vector3.x, (int)vector3.z) { }
}
[System.Serializable]
public class Doors
{
    [SerializeField] public int n;
    [SerializeField] public int e;
    [SerializeField] public int s;
    [SerializeField] public int w;
    public Doors(List<bool> hasDoor)
    {
        n = hasDoor[0] ? 1 : 0;
        e = hasDoor[1] ? 1 : 0;
        s = hasDoor[2] ? 1 : 0;
        w = hasDoor[3] ? 1 : 0;
    }

    public bool[] ToArray()
    {
        bool[] bools = new bool[4];
        bools[0] = n == 1 ? true : false;
        bools[1] = e == 1 ? true : false;
        bools[2] = s == 1 ? true : false;
        bools[3] = w == 1 ? true : false;
        return bools;
    }
}