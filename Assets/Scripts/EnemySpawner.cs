using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]    private int quantSpawnBase;
    [SerializeField]    private Transform enemiesParent;
    [SerializeField]    private GameObject enemie;
    [SerializeField]    private GameObject boss;
    [SerializeField]    private float delayAfterSpawn;
    [SerializeField]    private Path[] paths;
    [SerializeField]    private int IncreaseAmount;
    [SerializeField]    private Text waves;
    int hpIncrease = 0;
    private int countWaves = 0;

    private int maxSpawn;
    private Queue<GameObject> inimigos = new Queue<GameObject>();
    private GameObject bossInstance;
    void Awake()
    {
        for(int i = 0; i < quantSpawnBase; i++)
        {
            GameObject inimigo = Instantiate(enemie,enemiesParent);
            inimigos.Enqueue(inimigo);
        }
        bossInstance = Instantiate(boss, enemiesParent);

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
                Change();
                yield return new WaitForSecondsRealtime(3f);
                maxSpawn = quantSpawnBase + 4*i;
                while(maxSpawn > 0)
                {
                    int m = Random.Range(0, paths.Length);
                    if (!inimigos.Any())
                        inimigos.Enqueue(Instantiate(enemie, enemiesParent));
                    GameObject enemy = inimigos.Dequeue();
                    IEnemy enemyScript = enemy.GetComponent<IEnemy>();
                    enemyScript.Path = paths[m].points;
                    enemyScript.ChangeStats(i,hpIncrease);
                    enemy.SetActive(true);
                    maxSpawn--;
                    yield return new WaitForSecondsRealtime(delayAfterSpawn);
                }
                yield return new WaitUntil(()=>GameObject.FindGameObjectsWithTag("Enemy").Length < 1);
                Currency.Increase(IncreaseAmount);
            }
            yield return new WaitForSecondsRealtime(3f);

            Change();
            int n = Random.Range(0, paths.Length);
            IEnemy bossScript = bossInstance.GetComponent<IEnemy>();
            bossScript.Path = paths[n].points;
            bossScript.ChangeStats(countWaves, hpIncrease);
            bossInstance.SetActive(true);
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length < 1);
            Currency.Increase(IncreaseAmount*2);
            hpIncrease += 5;
        }
    }
    void Change()
    {
        waves.text = $"Wave: {++countWaves}";
    }
    public void Add(GameObject enemy)
    {
        this.inimigos.Enqueue(enemy);
        enemy.SetActive(false);
    }
}

[System.Serializable]
public struct Path
{
    public Transform[] points;
}