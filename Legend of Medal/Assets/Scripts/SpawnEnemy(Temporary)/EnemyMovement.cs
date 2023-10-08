using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject targetObject;
    public float speed = 5.0f;

    private void Update()
    {
        if (targetObject != null)
        {
            Transform targetTransform = targetObject.transform;

            // Create a new target position that retains the enemy's current y value
            Vector3 targetPositionWithSameY = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);

            Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPositionWithSameY, speed * Time.deltaTime);
            transform.position = nextPosition;

            if (Vector3.Distance(transform.position, targetPositionWithSameY) < 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogWarning("No target object set for enemy!");
        }
    }
}
