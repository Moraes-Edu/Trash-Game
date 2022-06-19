using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]    private int quantSpawnBase;
    [SerializeField]    private Transform[] spawnPoints;
    [SerializeField]    private GameObject enemie;
    [SerializeField]    private GameObject boss;
    [SerializeField]    private float delayAfterSpawn;
    [SerializeField]    private Path[] paths;
    [SerializeField]    private int IncreaseAmount;
    [SerializeField] private Text waves;
    int hpIncrease = 0;
    private int countWaves = 0;

    private int maxSpawn;

    void Start()
    {
        maxSpawn = quantSpawnBase;
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSecondsRealtime(3f);
        while (true)
        {
            for(int i = 1; i < 5; i++)
            {
                countWaves++;
                Change();
                yield return new WaitForSecondsRealtime(3f);
                maxSpawn = quantSpawnBase + 4*i;
                while(maxSpawn > 0)
                {
                    int m = Random.Range(0, spawnPoints.Length);
                    GameObject inimigo = Instantiate(enemie,spawnPoints[m]);

                    IEnemy a = inimigo.GetComponent<IEnemy>();
                    a.SetPath(paths[m].points);
                    a.ChangeStats(i,hpIncrease);
                    maxSpawn--;
                    yield return new WaitForSecondsRealtime(delayAfterSpawn);
                }
                yield return new WaitUntil(()=>GameObject.FindGameObjectsWithTag("Enemy").Length < 1);
                Currency.Increase(IncreaseAmount);
            }
            yield return new WaitForSecondsRealtime(3f);

            countWaves++;
            Change();
            int n = Random.Range(0, spawnPoints.Length);
            GameObject chefe = Instantiate(boss, spawnPoints[n]);
            IEnemy b = chefe.GetComponent<IEnemy>();
            b.SetPath(paths[n].points);
            b.ChangeStats(5,hpIncrease);
           
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length < 1);
            Currency.Increase(IncreaseAmount*2);
            hpIncrease += 5;
        }
    }
    void Change()
    {
        waves.text = $"Wave: {countWaves}";
    }
}

[System.Serializable]
public struct Path
{
    public Transform[] points;
}