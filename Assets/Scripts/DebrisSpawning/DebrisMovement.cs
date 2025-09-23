using System;
using UnityEngine;

public class DebrisMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    void Update()
    {
        transform.Translate(-Vector3.right * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
            case "Blackhole":
                Destroy(gameObject);
                break;
        }
        
    }
}
