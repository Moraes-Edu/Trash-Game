using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]    private int quantSpawn;
    [SerializeField]    private Transform[] spawnPoints;
    [SerializeField]    private GameObject enemie;
    [SerializeField]    private GameObject Boss;
    [SerializeField]    private float delayAfterSpawn;

    private int maxSpawn;
    public static int TextureIndex { get; private set; } = 0;
    bool gameOver = false;
    void Start()
    {
        maxSpawn = quantSpawn;
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (!gameOver)
        {
            while(maxSpawn > 0)
            {
                Instantiate(enemie,spawnPoints[Random.Range(0,spawnPoints.Length)]);
                maxSpawn--;
                yield return new WaitForSeconds(delayAfterSpawn);
            }
            yield return new WaitUntil(()=>GameObject.FindGameObjectsWithTag("Enemy").Length < 1);
            maxSpawn = quantSpawn;
        }
    }
}
