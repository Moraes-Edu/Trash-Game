using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 1;
    private Transform target;

    public float speed = 100f;

    public void Seek (Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Destroy(gameObject);
        target.GetComponent<IEnemy>().TakeDamage(damage);
    }
    public void Change(int multiplier)
    {
        damage *= multiplier;
    }
}
