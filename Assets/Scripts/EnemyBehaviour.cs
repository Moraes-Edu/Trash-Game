using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    protected int dano;
    const float turnSpeed = 10f;
    [SerializeField]
    protected int initialLife = 1;
    private float velocity = 5;
    private protected int life;
    public Transform[] Points { get; private set; }
    private int indexer;
    const float range = 2;
    EnemySpawner spawner;

    void Awake()
    {
        life = initialLife;
        spawner = GameObject.Find("WaveController").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        Vector3 dir = Points[indexer].position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        transform.Translate(Time.deltaTime * velocity * dir.normalized, Space.World);
        
        if (Vector3.Distance(Points[indexer].position, transform.position) < range)
        {
            if (++indexer >= Points.Length)
                Collide();
        }
    }
    void Collide()
    {
        HealthBehaviour.Decrease(dano);
        spawner.Add(this.gameObject);
    }
    public void TakeDamage(int dmg)
    {
        life -= dmg;
        if (life <= 0)
            spawner.Add(this.gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
    public void SetPath(Transform[] path)
    {
        this.Points = path;
    }
}
