using UnityEngine;

public class ObstaclesSpawn : MonoBehaviour
{
    [Header("Obstacle Spawn Settings")]
    [SerializeField] GameObject[] obstacles;
    [SerializeField] float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(obstacles[randomIndex], transform.position, Quaternion.identity);
    }
}
