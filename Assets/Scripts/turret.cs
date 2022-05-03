using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    private Transform target;
    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform partePraGirar;
    public float turnSpeed = 10f;
    void Start()
    {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
    }
      
    void UpdateTarget ()
    {
       GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
       float shorthestDistance = Mathf.Infinity;
       GameObject nearestEnemy = null;  

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
            if (distanceToEnemy < shorthestDistance)
            {
                shorthestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
       
       if (nearestEnemy != null && shorthestDistance <= range)
       {
          target = nearestEnemy.transform;
       }  else
       {
          target = null;
       }

    }
    void Update()
    {
        if (target == null)
        return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partePraGirar.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partePraGirar.rotation = Quaternion.Euler (0f, rotation.y, 0f);

    }

   void OnDrawGizmosSelected ()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, range);    
    }


}
