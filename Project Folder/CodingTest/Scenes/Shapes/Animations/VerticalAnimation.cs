using CodingTest.Rendering.Transform;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTest.Animations
{
    class VerticalAnimation : Animation
    {
        public int MaxHeight { get; set; }
        public int MinHeight { get; set; }

        private int m_direction = 1;
        public VerticalAnimation(float movementRate) : base(movementRate, "Vertical")
        {
            MaxHeight = 500;
            MinHeight = 100;
        }

        public override void UpdateAnimation(Transform2D transform, float deltaTime)
        {
            var currentPosition = transform.Position;

            currentPosition.Y += m_direction * MovementRate * deltaTime;

            if (currentPosition.Y > MaxHeight)
            {
                currentPosition.Y = MaxHeight;
                m_direction = -1;
            }

            if (currentPosition.Y < MinHeight)
            {
                currentPosition.Y = MinHeight;
                m_direction = 1;
            }

            transform.Position = currentPosition;
        }
    }
}
