using CodingTest.Rendering.Transform;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTest.Animations
{
    abstract class Animation
    {
        /// <summary>
        /// Pixel movement rate of the animation
        /// </summary>
        public float MovementRate { get; set; }
        /// <summary>
        /// String to identify type of animation.
        /// </summary>
        public string AnimationType { get; private set; }
        protected Animation(float movementRate, string animationType)
        {
            MovementRate = movementRate;
            AnimationType = animationType;
        }

        /// <summary>
        /// Callback used to play an animation
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="deltaTime"></param>
        public abstract void UpdateAnimation(Transform2D transform, float deltaTime);
    }
}
