using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField]    private Transform[] spawnPoints;
    [SerializeField]    private GameObject enemie;
    [SerializeField]    private Queue<GameObject> enemiesQueue;
    [SerializeField]    private float delayAfterSpawn;
    bool gameOver = false;

    private void Awake()
    {
        enemiesQueue = new Queue<GameObject>();
        for(int i = 0; i < WaveBehaviour.quantSpawn; i++)
        {
            enemiesQueue.Enqueue(Instantiate(enemie,spawnPoints[Random.Range(0,spawnPoints.Length)]));
        }
    }
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (!gameOver)
        {
            while (enemiesQueue.Count > 0)
            {
                yield return new WaitForSeconds(delayAfterSpawn);
                enemiesQueue.Dequeue().SetActive(true);
                WaveBehaviour.contSpawn--;
            }
        }
    }
}
