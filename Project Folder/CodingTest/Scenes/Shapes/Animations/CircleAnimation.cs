using CodingTest.Rendering.Transform;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CodingTest.Animations
{
    class CircleAnimation : Animation
    {
        private float m_radius;
        private Vector2 m_centre;
        private float m_currentAngle;

        public CircleAnimation(Vector2 centre, float radius, float movementRate) : base(movementRate, "Circle")
        {
            m_radius = radius;
            m_centre = centre;
        }

        public override void UpdateAnimation(Transform2D transform, float deltaTime)
        {
            m_currentAngle += MovementRate * deltaTime;

            //Finds target position by just spinning a vector by the current angle
            Vector2 targetPosition = m_centre + Vector2.Transform(new Vector2(0, m_radius), Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), m_currentAngle));

            transform.Position = targetPosition;
        }
    }
}
