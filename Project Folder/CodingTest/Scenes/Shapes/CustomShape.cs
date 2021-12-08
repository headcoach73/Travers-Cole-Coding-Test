using CodingTest.Rendering.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CodingTest.Shapes
{
    class CustomShape : Shape
    {
        private float[] m_customVertices;
        private float[] m_customVertexColors;

        /// <summary>
        /// Creates a shape from given custom vertices and vertexColors.
        /// </summary>
        /// <param name="shader"></param>
        /// <param name="vertices"></param>
        /// <param name="vertexColors"></param>
        public CustomShape(Shader shader, float[] vertices, float[] vertexColors) : base(shader, Color.White, "CustomShape") //Set color to white because its gonna be ignored anyway
        {
            m_customVertices = vertices;
            m_customVertexColors = vertexColors;
        }

        public override float[] DefineVertexColors()
        {
            return m_customVertexColors;
        }

        public override float[] DefineVertices()
        {
            return m_customVertices;
        }
    }
}
