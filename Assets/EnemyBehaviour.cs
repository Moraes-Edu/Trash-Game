using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    Transform points;
    NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        points = GameObject.FindGameObjectWithTag("Objective").transform;
    }
    void Update()
    {
        agent.SetDestination(points.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Objective")
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
