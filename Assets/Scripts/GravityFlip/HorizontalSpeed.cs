using System;
using UnityEngine;

public class HorizontalSpeed : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(playerSpeed, _rigidbody.linearVelocity.y);
    }
}
