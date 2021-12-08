using CodingTest.Rendering.Transform;

namespace CodingTest.Animations
{
    class HorizontalAnimation : Animation
    {
        public int MaxWidth { get; set; }
        public int MinWidth { get; set; }

        private int m_direction = 1;
        public HorizontalAnimation(float movementRate) : base(movementRate, "Horizontal")
        {
            MaxWidth = 500;
            MinWidth = 100;
        }

        public override void UpdateAnimation(Transform2D transform, float deltaTime)
        {
            var currentPosition = transform.Position;

            currentPosition.X += m_direction * MovementRate * deltaTime;

            if (currentPosition.X > MaxWidth)
            {
                currentPosition.X = MaxWidth;
                m_direction = -1;
            }

            if (currentPosition.X < MinWidth)
            {
                currentPosition.X = MinWidth;
                m_direction = 1;
            }

            transform.Position = currentPosition;
        }
    }
}
