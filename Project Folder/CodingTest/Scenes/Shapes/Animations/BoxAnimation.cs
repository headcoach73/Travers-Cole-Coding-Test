using CodingTest.Rendering.Transform;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CodingTest.Animations
{
    class BoxAnimation : Animation
    {
        private Vector2 m_topLeftCorner;
        private Vector2 m_bottomRightCorner;

        private Vector2 m_targetCorner;
        private Vector2 m_direction;
        private int m_currentIndex = 0;

        public BoxAnimation(Vector2 topLeftCorner, Vector2 bottomLeftCorner, float movementRate) : base(movementRate, "Box")
        {
            m_topLeftCorner = topLeftCorner;
            m_bottomRightCorner = bottomLeftCorner;

            m_targetCorner = m_topLeftCorner;
            m_direction = new Vector2(0, 1);
        }

        public override void UpdateAnimation(Transform2D transform, float deltaTime)
        {
            var nextPosition = transform.Position + (m_direction * MovementRate * deltaTime);

            var currentDot = Vector2.Dot(m_direction, nextPosition);
            var targetDot = Vector2.Dot(m_direction, m_targetCorner);
            bool destinationReached = currentDot > targetDot;

            if (destinationReached)
            {
                //Set position so we don't exceed the corner
                nextPosition = m_targetCorner;

                m_currentIndex++;
                m_currentIndex = m_currentIndex % 4; //This resets index to 0 past 3.
                switch (m_currentIndex)
                {
                    case 0:
                        {
                            m_targetCorner = m_topLeftCorner;
                            m_direction = new Vector2(0, -1);
                            break;
                        }
                    case 1:
                        {
                            m_targetCorner.X = m_bottomRightCorner.X;
                            m_direction = new Vector2(1, 0);
                            break;
                        }
                    case 2:
                        {
                            m_targetCorner = m_bottomRightCorner;
                            m_direction = new Vector2(0, 1);
                            break;
                        }
                    case 3:
                        {
                            m_targetCorner.X = m_topLeftCorner.X;
                            m_direction = new Vector2(-1, 0);
                            break;
                        }
                    default:
                        break;
                }
            }

            transform.Position = nextPosition;
        }
    }
}
