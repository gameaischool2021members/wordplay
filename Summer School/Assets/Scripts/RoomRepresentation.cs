using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class RoomRepresentation
{
    [SerializeField] public int roomNumb;
    //[SerializeField] public string otherInfo;
    [SerializeField] public Vector3 center;
    //[SerializeField] public float size = 20;
    [SerializeField] public List<bool> doors = new List<bool>();
    [SerializeField] public DesignType design;
    [SerializeField] public int difficulty;

    public RoomRepresentation(RoomBehaviour room, int id)
    {
        roomNumb = id;
        center = room.transform.position;
        center.y = 0;
        doors = room.hasDoor;
        int enumLengh = System.Enum.GetValues(typeof(DesignType)).Length;
        design = (DesignType)UnityEngine.Random.Range(0, enumLengh);
        difficulty = UnityEngine.Random.Range(0, 10);
        //otherInfo = info;
    }

}

public enum DesignType
{
    design1,
    design2,
    design3
}


