using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] spawnLocations;
    public GameObject targetObject; // The cube or any other object you want as the target.
    public float spawnInterval = 5.0f;
    public float initialSpawnDelay = 10.0f;

    private void Start()
    {
        if (spawnLocations.Length == 0)
        {
            Debug.LogError("No spawn locations set!");
            return;
        }

        InvokeRepeating("SpawnEnemies", initialSpawnDelay, spawnInterval);
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (spawnLocations[i] != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, spawnLocations[i].transform.position, Quaternion.identity);

                // Set the target for the instantiated enemy
                EnemyMovement enemyMovement = enemyInstance.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.targetObject = targetObject;
                }
            }
            else
            {
                Debug.LogWarning("Spawn location object at index " + i + " is not set!");
            }
        }
    }
}
