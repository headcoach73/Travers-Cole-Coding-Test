using CodingTest.Rendering.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CodingTest.Shapes
{
    class Triangle : Shape
    {
        private int m_width;
        private int m_height;

        public Triangle(Shader shader, Color color, int width, int height) : base(shader, color, "Triangle")
        {
            m_width = width;
            m_height = height;
        }

        public override float[] DefineVertexColors()
        {
            float[] colorVertices =
{
                m_color.R, m_color.G, m_color.B, // top
                m_color.R, m_color.G, m_color.B, // bottom left
                m_color.R, m_color.G, m_color.B, // bottom right
            };

            return colorVertices;
        }

        public override float[] DefineVertices()
        {
            float[] vertices =
            {
                0, -0.5f * m_height, // top
                -0.5f * m_width, 0.5f * m_height, // bottom left
                0.5f * m_width, 0.5f * m_height, // bottom right
            };

            return vertices;
        }
    }
}
