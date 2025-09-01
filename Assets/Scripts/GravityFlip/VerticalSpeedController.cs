using System;
using UnityEngine;
using InputSystem;
using UnityEngine.InputSystem;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Gravity
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class VerticalSpeedController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 1f;
        private Rigidbody2D _rigidbody;
        private InputController _playerInputSystem;
        private InputAction _jumpControl;
        private float _time = 0f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float rayDist = 1.5f;
        [SerializeField] private KinematicFunction kinematicFunctions = KinematicFunction.Tangential;
        [SerializeField] private float functionMultiplier = 1f;
        [SerializeField] private bool manualGravityFlip = false;
        private bool _isGravityFlipped = false;
        private Func<float,float,bool,float> _kinematicFunction;

        private void Awake()
        {
            _playerInputSystem = new InputController();
        }

        private void OnEnable()
        {
            _jumpControl = _playerInputSystem.Player.FlipGravity;
            _jumpControl.performed += FlipGravity;
            _jumpControl.Enable();
        }

        private void OnDisable()
        {
            _jumpControl.performed -= FlipGravity;
            _jumpControl.Disable();
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
            GetKinematicFunction();
        }

        private void OnValidate()
        {
            GetKinematicFunction();
        }
        
        private void GetKinematicFunction()
        {
            _kinematicFunction = kinematicFunctions switch
            {
                KinematicFunction.Tangential => KinematicFunctions.TangentialVelocity,
                KinematicFunction.Linear => KinematicFunctions.LinearVelocity,
                KinematicFunction.Exponential => KinematicFunctions.ExponentialVelocity,
                KinematicFunction.Logarithmic => KinematicFunctions.LogarithmicVelocity,
                KinematicFunction.Sigmoid => KinematicFunctions.SigmoidVelocity,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (_kinematicFunction == KinematicFunctions.TangentialVelocity)
            {
                Debug.LogWarning("Kinematic Function: Function result for tangential function are clamped so the velocity will never go over 1.57 * speed");
            }
        }


        private void FixedUpdate()
        {
            int direction = _isGravityFlipped ? 1 : -1;
            Vector2 rayDirection = direction == 1 ? Vector2.up : Vector2.down;
            bool grounded = Physics2D.Raycast(transform.position, rayDirection, 1.5f, groundLayer);
            Debug.DrawRay(transform.position, rayDirection * 1.5f, grounded ? Color.green : Color.red);

            if (!grounded)
            {
                _rigidbody.linearVelocityY = _kinematicFunction(_time, functionMultiplier, true) * playerSpeed * direction;
                _time += Time.fixedDeltaTime;
            }
        }

        private void FlipGravity(InputAction.CallbackContext context)
        {
            _isGravityFlipped = !_isGravityFlipped;
            _time = 0;
        }
        
        
        // Debug Info++
        private float _lastCollisionTime = -1f;
        private void OnCollisionEnter2D(Collision2D other)
        {

            if (1 << other.gameObject.layer == groundLayer.value)
            {
                if (_lastCollisionTime >= 0)
                {
                    float timeBetween = Time.fixedTime - _lastCollisionTime;
                    Debug.Log($"Time between each collision to wall: {timeBetween}");
                }
                _lastCollisionTime = Time.fixedTime;
                if (manualGravityFlip)
                {
                    FlipGravity(new InputAction.CallbackContext());
                }
            }
        }
        // Debug Info--
        
    }
    
    public enum KinematicFunction
    {
        Tangential,
        Linear,
        Exponential,
        Logarithmic,
        Sigmoid
    }
}