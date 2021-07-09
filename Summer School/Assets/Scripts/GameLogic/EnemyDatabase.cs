using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "ScriptableObjects/EnemyDatabase", order = 1)]
public class EnemyDatabase : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;

    public GameObject GetRandomEnemy() => enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
}
