using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField]    private Transform[] spawnPoints;
    [SerializeField]    private GameObject[] enemies;
    [SerializeField]    private Queue<GameObject> enemiesQueue;
    [SerializeField]    private int enemiesNumber;
    [SerializeField]    private float delayAfterSpawn;

    private void Awake()
    {
        enemiesQueue = new Queue<GameObject>();
        for(int i = 0; i < enemiesNumber; i++)
        {
            enemiesQueue.Enqueue(Instantiate(enemies[Random.Range(0 ,enemies.Length)],spawnPoints[Random.Range(0,spawnPoints.Length)]));
        }
    }
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (enemiesQueue.Count > 0)
        {
            yield return new WaitForSeconds(delayAfterSpawn);
            enemiesQueue.Dequeue().SetActive(true);
        }
    }
}
