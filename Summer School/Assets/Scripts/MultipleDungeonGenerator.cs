using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MultipleDungeonGenerator : MonoBehaviour
{
    public GameObject templatePrefab;
    //public int dungeons2Generate=50000;
    public int dungeons2Generate = 5;
    private void Start()
    {
        StartCoroutine(DungeonGenerator());
    }

    IEnumerator DungeonGenerator()
    {
        Debug.Log(Application.dataPath);
        yield return null;

        for (int i = 0; i < dungeons2Generate; i++)
        {
            // create the template.
            RoomTemplates template = Instantiate(templatePrefab, Vector3.zero, Quaternion.identity).GetComponent<RoomTemplates>();
            // wait for 2.5 seconds
            yield return new WaitForSeconds(0.9f);

            // Write to disk
            string path = Application.dataPath + "/Dungeons";

            StreamWriter writer = new StreamWriter(path + "/" + i + ".txt", true);
            Debug.Log(JsonUtility.ToJson(template.dungeon));
            writer.Write(JsonUtility.ToJson(template.dungeon));
            writer.Close();

            // delete.
            Destroy(template.gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
