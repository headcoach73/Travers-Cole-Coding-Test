using CodingTest.Rendering.Transform;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CodingTest.Animations
{
    class CustomAnimation : Animation
    {
        public List<Vector2> PositionSequence => m_positions;
        private List<Vector2> m_positions;
        private int m_currentIndex = 0;
        private Vector2 CurrentTargetPosition => m_positions[m_currentIndex];

        private const float POSITION_SESNSITIVITY = 3f; //Roughly 3 pixels

        /// <summary>
        /// Custom animation just follows a sequence of positions in a loop
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="movementRate"></param>
        public CustomAnimation(List<Vector2> positions, float movementRate) : base(movementRate, "Custom")
        {
            m_positions = positions;

        }

        public override void UpdateAnimation(Transform2D transform, float deltaTime)
        {
            if (m_positions.Count == 0) return;

            Vector2 direction = Vector2.Normalize(CurrentTargetPosition - transform.Position);

            transform.Position += direction * MovementRate * deltaTime;

            if ((CurrentTargetPosition - transform.Position).LengthSquared() < POSITION_SESNSITIVITY * POSITION_SESNSITIVITY)
            {
                transform.Position = CurrentTargetPosition;
                m_currentIndex++;
                m_currentIndex = m_currentIndex % m_positions.Count;
            }
        }
    }
}
