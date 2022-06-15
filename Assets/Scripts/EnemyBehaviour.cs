using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]    private float velocity;
    [SerializeField]    private int life;
    public Transform[] points;
    private int indexer = 0;

    void Update()
    {
        Vector3 dir = points[indexer].position - transform.position;
        transform.Translate(dir.normalized * velocity * Time.deltaTime, Space.World);

        if (Vector3.Distance(points[indexer].position, transform.position) < 10)
        {
            if (points.Length < indexer)
                indexer++;
            else
                Collide();
        }
    }
    void Collide()
    {
        HealthBehaviour.hp--;
        Destroy(this.gameObject);
    }
}
