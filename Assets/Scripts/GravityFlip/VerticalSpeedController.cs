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
        [Header("Raycast Settings")]
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask groundLayer;
        
        [Header("Kinematic Settings")]
        [SerializeField] private KinematicFunction kinematicFunctions = KinematicFunction.Exponential;
        [SerializeField] private float playerSpeed = 1f;
        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 10f;
        [SerializeField] private float timeToReachMaxSpeed = 1f;
        [SerializeField] private float smootherConstant = 1f;
        private Func<float,float,float,float,float,float> _kinematicFunction;
        
        [Header("Debug Settings")]
        [SerializeField] private bool manualGravityFlip = false;
        
        // direction of player vertical movement
        private GravityDirection _gravityDirection = GravityDirection.Down;
        // local rigidbody
        private Rigidbody2D _rigidbody;
        // Input System
        private InputController _playerInputSystem;
        private InputAction _jumpControl;
        // local time
        private float _time = 0f;
        

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
            // ensure that kinematic function is updated in editor when changed
            GetKinematicFunction();
        }
        
        private void GetKinematicFunction()
        {
            // convert enum to function
            _kinematicFunction = kinematicFunctions switch
            {
                KinematicFunction.Linear => KinematicFunctions.LinearVelocityNormalized,
                KinematicFunction.LinearDecay => KinematicFunctions.LinearDecayVelocityNormalized,
                KinematicFunction.Exponential => KinematicFunctions.ExponentialVelocityNormalized,
                KinematicFunction.ExponentialDecay => KinematicFunctions.ExponentialDecayVelocityNormalized,
                _ => throw new ArgumentOutOfRangeException()
            };
        }


        private void FixedUpdate()
        {
            int direction = (int)_gravityDirection;
            Vector2 rayDirection = new Vector2(0, direction);
            bool grounded = Physics2D.Raycast(transform.position, rayDirection, rayDistance, groundLayer);
            // Debug ++
            Debug.DrawRay(transform.position, rayDirection * rayDistance, grounded ? Color.green : Color.red);
            // Debug --
            if (!grounded)
            {
                _rigidbody.linearVelocityY = _kinematicFunction(_time, minSpeed, maxSpeed, timeToReachMaxSpeed, smootherConstant) * direction;
                _time += Time.fixedDeltaTime;
            }
            
            if (_time > timeToReachMaxSpeed)
            {
                _time = timeToReachMaxSpeed;
            }
        }

        private void FlipGravity(InputAction.CallbackContext context)
        {
            // flip gravity direction
            _gravityDirection = _gravityDirection == GravityDirection.Down ? GravityDirection.Up : GravityDirection.Down;
            _time = 0; // resets time so that kinematic function starts from 0
        }
        
        
        // Debug ++
        private float _lastCollisionTime = -1f;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (1 << other.gameObject.layer != groundLayer.value) return;
            if (_lastCollisionTime >= 0)
            {
                var timeBetween = Time.fixedTime - _lastCollisionTime;
                Debug.Log($"Time between each collision to wall: {timeBetween}");
            }
            _lastCollisionTime = Time.fixedTime;
            if (manualGravityFlip)
            {
                FlipGravity(new InputAction.CallbackContext());
            }
        }
        // Debug --
        
    }
    
    public enum KinematicFunction
    {
        Linear,
        LinearDecay,
        Exponential,
        ExponentialDecay
    }
    public enum GravityDirection
    {
        Up = -1,
        Down = 1
    }
}