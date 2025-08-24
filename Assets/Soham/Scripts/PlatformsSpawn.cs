using UnityEngine;

public class PlatformsSpawn : MonoBehaviour
{
    [Header("Platform Spawn Settings")]
    [SerializeField] GameObject[] platforms;
    [SerializeField] Transform[] platformSpawnPoints;
    [SerializeField] float spawnInterval;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPlatform), 0f, spawnInterval);
    }

    private void SpawnPlatform()
    {
        int randomIndex = Random.Range(0, platforms.Length);
        int randomSpawnPointIndex = Random.Range(0, platformSpawnPoints.Length);
        spawnInterval = Random.Range(2f, 5f);
        GameObject platform = Instantiate(platforms[randomIndex], platformSpawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
    }
}
