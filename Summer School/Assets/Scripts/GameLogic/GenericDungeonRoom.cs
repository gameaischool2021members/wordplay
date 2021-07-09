using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDungeonRoom : MonoBehaviour
{
    [SerializeField] private EnemyDatabase enemies;
    [SerializeField] private GameObject[] closedWalls;
    [SerializeField] private GameObject[] openWalls;
    [SerializeField] public bool[] hasbeenConnected;
    [SerializeField] private List<GenericDungeonRoom> neighbours = new List<GenericDungeonRoom>();
    public IntVector2 roomPosition;
    public void InitializeRoom(bool[] hasDoor, IntVector2 pos, int numberOfEnemies)
    {
        roomPosition = pos;
        for (int i = 0; i < hasDoor.Length; i++)
        {
            closedWalls[i].SetActive(!hasDoor[i]);
            openWalls[i].SetActive(hasDoor[i]);
            hasbeenConnected[i] = !hasDoor[i];
        }
        //NO Door Room
        if (!hasDoor[0] && !hasDoor[1] && !hasDoor[2] && !hasDoor[3])
            gameObject.SetActive(false);

        //Instantiate Enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = enemies.GetRandomEnemy();
            Vector3 enemyPosition = new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));
            //Vector3 enemyPosition = new Vector3(Random.Range(-0.4f, 0.4f), 0, Random.Range(-0.4f, 0.4f));
            enemyPosition = enemyPosition + transform.position;
            enemyPosition.y = enemy.transform.position.y;
            Instantiate(enemy, enemyPosition, Quaternion.identity).transform.SetParent(transform);
        }

    }

    public void AddUniqueNeighbour(GenericDungeonRoom room)
    {
        if (neighbours.Contains(room))
            return;
        neighbours.Add(room);
    }

    public void CloseDoor(int index)
    {
        closedWalls[index].SetActive(true);
        openWalls[index].SetActive(false);
    }
}
