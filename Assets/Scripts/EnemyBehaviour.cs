using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    Material[] materiais;
    Transform points;
    NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        points = GameObject.FindGameObjectWithTag("Objective").transform;
        var a = transform.GetChild(0);
        for(int i = 0; i < a.childCount; i++)
        {
            a.transform.GetChild(i).GetComponent<MeshRenderer>().material = materiais[WaveBehaviour.index];
        }
    }
    void Update()
    {
        agent.SetDestination(points.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Objective")
        {
            HealthBehaviour.hp--;
            Destroy(this.gameObject);
        }
    }
}
