using UnityEngine;

public class FixedMonster : MonoBehaviour
{
    public float range = 5f; // 攻击范围
    public float fireRate = 1f; // 射击频率
    private float fireCountdown = 0f;

    void Update()
    {
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            if (fireCountdown <= 0f)
            {
                Shoot(nearestEnemy);
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot(GameObject enemy)
    {
      
    }
}
