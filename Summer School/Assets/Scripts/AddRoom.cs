using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;
    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
