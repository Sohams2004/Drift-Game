using UnityEngine;

public class BackgroundSpawn : MonoBehaviour
{
    [Header("BagGround Spawn Settings")]
    [SerializeField] GameObject[] backGrounds;
    [SerializeField] Transform[] backGroundSpawnPoints;
    [SerializeField] float spawnInterval = 2f;
    
    private void Start()
    {
        InvokeRepeating(nameof(SpawnBackGround), 0f, spawnInterval);
    }
    
    private void SpawnBackGround()
    {
        int randomIndex = Random.Range(0, backGrounds.Length);
        GameObject backGroundUp = Instantiate(backGrounds[0], backGroundSpawnPoints[0].position, Quaternion.identity);
        GameObject backGroundDown = Instantiate(backGrounds[1], backGroundSpawnPoints[1].position, Quaternion.Euler(new Vector3(180,0,0)));
    }
}
