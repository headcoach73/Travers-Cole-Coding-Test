using CodingTest.Rendering.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace CodingTest.Shapes
{
    class Circle : Shape
    {
        private float m_radius;
        private int m_triangleCount;

        /// <summary>
        /// The higher the triangle count the smoother the circle.
        /// </summary>
        /// <param name="shader"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        /// <param name="triangleCount"></param>
        public Circle(Shader shader, Color color, float radius, int triangleCount) : base(shader, color, triangleCount != 6? "Circle" : "Hexagon") //Hexagon inherits from this class
        {
            m_radius = radius;
            m_triangleCount = triangleCount;
        }

        public override float[] DefineVertexColors()
        {
            //Just creates an array of color vertices using the shape color

            List<float> vertexColors = new List<float>();
            for (int i = 0; i < m_vertices.Length / 2; i++)
            {
                vertexColors.Add(m_color.R);
                vertexColors.Add(m_color.G);
                vertexColors.Add(m_color.B);
            }
            return vertexColors.ToArray();
        }

        public override float[] DefineVertices()
        {
            //This creates a fan of triangles used as an approximation of a circle
            //Higher triangle counts provide smoother circle

            List<float> vertices = new List<float>();
            float x1 = 0f;
            float y1 = m_radius;
            float phi = 2f * MathF.PI / m_triangleCount;
            for (int i = 1; i < m_triangleCount + 1; i++)
            {
                //Origin Vertex
                vertices.Add(0);
                vertices.Add(0);

                float x2 = m_radius * MathF.Sin(i * phi);
                float y2 = m_radius * (float)Math.Cos(i * phi);

                //New pos Vertex
                vertices.Add(x2);
                vertices.Add(y2);

                //Last Pos vertex
                vertices.Add(x1);
                vertices.Add(y1);

                x1 = x2;
                y1 = y2;
            }
            return vertices.ToArray();
        }
    }
}
