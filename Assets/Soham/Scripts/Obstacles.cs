using System;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] float obstacleMoveSpeed;

    void MoveObstacle()
    {
        gameObject.transform.position += Vector3.left * obstacleMoveSpeed * Time.deltaTime;
    }

    private void Update()
    {
        MoveObstacle();
    }
}
