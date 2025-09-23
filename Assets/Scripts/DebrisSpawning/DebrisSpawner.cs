using System.Collections;
using UnityEngine;
using Library.Singletons;
public class DebrisSpawner : Singleton<DebrisSpawner>
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    
    private void Start()
    {
        InvokeRepeating(nameof(SpawnDebris) , 0f, spawnInterval);
    }
    private void SpawnDebris()
    {
        
        var randomPointx = Random.Range(spawnPoint1.position.x, spawnPoint2.position.x);
        var randomPointy = Random.Range(spawnPoint1.position.y, spawnPoint2.position.y);
        var spawnLocation = new Vector3(randomPointx, randomPointy, transform.position.z);
        
        Instantiate(obstaclePrefab, spawnLocation, Quaternion.identity);
    }
}
