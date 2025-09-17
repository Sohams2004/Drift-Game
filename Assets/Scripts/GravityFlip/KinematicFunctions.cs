using System;
using UnityEngine;

namespace Gravity
{
    public static class KinematicFunctions
    {
        // normalized equation: x * maxSpeed / timeToMaxSpeed
        public static float LinearVelocityNormalized(float time, float minSpeed, float maxSpeed, float timeToMaxSpeed, float smootherConstant)
        {
            var value = time * maxSpeed / timeToMaxSpeed;
            return Mathf.Min(Mathf.Max(value, minSpeed), maxSpeed);
        }
        
        // normalized equation: ((-x * maxSpeed) / timeToMaxSpeed) + maxSpeed
        public static float LinearDecayVelocityNormalized(float time, float minSpeed, float maxSpeed, float timeToMaxSpeed, float smootherConstant)
        {
            var value = -(time * maxSpeed / timeToMaxSpeed) + maxSpeed;
            return Mathf.Min(Mathf.Max(value, minSpeed), maxSpeed);
        }
        // normalized equation: maxSpeed * (e^(smootherConstant * x) - 1) / (e^(smootherConstant * timeToMaxSpeed) - 1)
        public static float ExponentialVelocityNormalized(float time, float minSpeed, float maxSpeed, float timeToMaxSpeed, float smootherConstant = 1f)
        {
            var value = maxSpeed * (Mathf.Exp(smootherConstant * time) - 1f) / (Mathf.Exp(smootherConstant * timeToMaxSpeed) - 1f);
            return Mathf.Min(Mathf.Max(value, minSpeed), maxSpeed);
            
        }
        
        // normalized equation: maxSpeed * (e^(smootherConstant * (timeToMaxSpeed - x)) - 1) / (e^(smootherConstant * timeToMaxSpeed) - 1)
        public static float ExponentialDecayVelocityNormalized(float time, float minSpeed, float maxSpeed, float timeToMaxSpeed, float smootherConstant = 1f)
        {
            var value = maxSpeed * (Mathf.Exp(smootherConstant * (timeToMaxSpeed - time)) - 1f) / (Mathf.Exp(smootherConstant * timeToMaxSpeed) - 1f);
            return Mathf.Min(Mathf.Max(value,minSpeed), maxSpeed);
        }
    }
}
