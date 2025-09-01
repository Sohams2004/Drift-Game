using System;
using UnityEngine;

namespace Gravity
{
    public static class KinematicFunctions
    {
        public static float TangentialVelocity(float time,float multiplier, bool isClamped = true)
        {
            var value = time * multiplier;
            if (isClamped)
                return Mathf.Clamp(value , 0f, Mathf.PI/2f);
            return MathF.Tan(value);
        }
        
        public static float LinearVelocity(float time,float multiplier, bool isClamped = true)
        {
            var value = time * multiplier;
            return value;
        }
        
        public static float ExponentialVelocity(float time,float multiplier, bool isClamped = true)
        {
            var value = time * multiplier;

            return Mathf.Exp(value) - 1f;
        }
        
        public static float LogarithmicVelocity(float time,float multiplier, bool isClamped = true)
        {
            var value = time * multiplier;

            return Mathf.Log(value + 1f);
        }
        public static float SigmoidVelocity(float time,float multiplier, bool isClamped = true)
        {
            var value = time * multiplier;

            return 1f / (1f + Mathf.Exp(-value));
        }
    }
}
