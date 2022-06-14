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
    [SerializeField]     private Transform childPaths;
    private Transform[][] paths;

    private int maxSpawn;
    public static int TextureIndex { get; private set; } = 0;
    bool gameOver = false;
    private int IncreaseAmount;

    void Start()
    {
        maxSpawn = quantSpawn;
        paths = new Transform[childPaths.childCount][];
        for(int i = 0; i < childPaths.childCount; i++)
        {
            Transform child = childPaths.GetChild(i);
            paths[i] = new Transform[child.childCount];
            for(int j = 0; j < child.childCount; j++)
            {
                paths[i][j] = child.GetChild(j);
            }
        }
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            while(maxSpawn > 0)
            {
                int n = Random.Range(0, spawnPoints.Length);
                GameObject inimigo = Instantiate(enemie,spawnPoints[n]);
                inimigo.GetComponent<EnemyBehaviour>().points = paths[n]; 
                maxSpawn--;
                yield return new WaitForSeconds(delayAfterSpawn);
            }
            yield return new WaitUntil(()=>GameObject.FindGameObjectsWithTag("Enemy").Length < 1);
            Currency.Increase(IncreaseAmount);
            maxSpawn = quantSpawn;
        }
    }
}
