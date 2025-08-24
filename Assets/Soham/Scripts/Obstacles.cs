using System;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] float obstacleMoveSpeed;
    [SerializeField] private float leftBound;

    private void Start()
    {
        Camera cam = Camera.main;
        leftBound = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x - gameObject.transform.lossyScale.x;
    }

    void MoveObstacle()
    {
        gameObject.transform.position += Vector3.left * obstacleMoveSpeed * Time.deltaTime;
        
        if (gameObject.transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        MoveObstacle();
    }
}
