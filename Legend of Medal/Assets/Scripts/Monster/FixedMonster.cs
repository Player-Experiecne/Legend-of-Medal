using UnityEngine;

public class FixedMonster : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 1f;
    public float turnSpeed = 10f;  // 新增的转向速度属性
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    private GameObject target; // 将目标存储为一个私有属性

    void Update()
    {
        // 选择目标的逻辑
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

        target = shortestDistance <= range ? nearestEnemy : null; // 如果有在范围内的敌人，则更新目标

        // 如果有目标，进行转向和射击
        if (target != null)
        {
            // 转向目标
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);  // 这里可以根据你的需求调整旋转的轴

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnEnable()
    {
        DefenderManager.Instance.RegisterDefender(gameObject);
    }

    private void OnDestroy()
    {
        DefenderManager.Instance.UnregisterDefender(gameObject);
    }

}
