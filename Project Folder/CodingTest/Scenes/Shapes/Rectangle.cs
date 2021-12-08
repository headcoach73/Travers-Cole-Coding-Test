using CodingTest.Rendering.Shaders;
using System.Drawing;

namespace CodingTest.Shapes
{
    class Rectangle : Shape
    {
        private int m_width;
        private int m_height;
        public Rectangle(Shader shader, Color color, int width, int height) : base(shader, color, "Rectangle")
        {
            m_width = width;
            m_height = height;
        }

        public override float[] DefineVertexColors()
        {
            float[] colorVertices =
            {
                m_color.R, m_color.G, m_color.B, // top left
                m_color.R, m_color.G, m_color.B, // top right
                m_color.R, m_color.G, m_color.B, // bottom left

                m_color.R, m_color.G, m_color.B, // top right
                m_color.R, m_color.G, m_color.B, // bottom right
                m_color.R, m_color.G, m_color.B, // bottom left
            };

            return colorVertices;
        }

        public override float[] DefineVertices()
        {
            float[] vertices =
            {
                -0.5f * m_width, 0.5f * m_height, // top left
                0.5f * m_width, 0.5f * m_height, // top right
                -0.5f * m_width, -0.5f * m_height, // bottom left

                0.5f * m_width, 0.5f * m_height, // top right
                0.5f * m_width, -0.5f * m_height, // bottom right
                -0.5f * m_width, -0.5f * m_height, // bottom left
            };

            return vertices;
        }
    }
}
