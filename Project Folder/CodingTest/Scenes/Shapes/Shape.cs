using CodingTest.Animations;
using CodingTest.Rendering.Cameras;
using CodingTest.Rendering.Shaders;
using CodingTest.Rendering.Transform;
using System;
using System.Drawing;
using static CodingTest.OpenGL.GL;

namespace CodingTest.Shapes
{
    abstract class Shape
    {
        /// <summary>
        /// Shape type used to identify what type of shape this is.
        /// </summary>
        public string ShapeType { get; private set; }

        /// <summary>
        /// Controls the position, scale and rotation of the shape.
        /// </summary>
        public Transform2D Transform { get; private set;}

        /// <summary>
        /// Current animation used for the shape. If not set no animation is played.
        /// </summary>
        public Animation Animation { get; set; }

        /// <summary>
        /// ShapeID used to identify which shape this is in the scene
        /// </summary>
        public string ShapeID { get; set; }

        /// <summary>
        /// Color used for shape. This is overridden in custom shapes.
        /// </summary>
        public Color Color { get => m_color;}

        /// <summary>
        /// Is shape loaded into memory ready to be rendered.
        /// </summary>
        public bool Loaded { get; private set; }

        protected Color m_color;

        private Shader m_shader;
        
        private uint m_vao;
        private uint m_vboVertices;
        private uint m_vboColors;
        protected float[] m_vertices;
        private float[] m_vertexColors;

        public Shape(Shader shader, Color color, string type)
        {
            m_shader = shader;
            m_color = color;
            ShapeType = type;
            Transform = new Transform2D();
        }

        public abstract float[] DefineVertices();
        public abstract float[] DefineVertexColors();

        /// <summary>
        /// Load vertices into memory so shape can be rendered.
        /// </summary>
        public unsafe void Load()
        {
            m_vao = glGenVertexArray();
            m_vboVertices = glGenBuffer();
            m_vboColors = glGenBuffer();

            //Bind vao
            glBindVertexArray(m_vao);

            //Vertices buffer object
            glBindBuffer(GL_ARRAY_BUFFER, m_vboVertices);

            m_vertices = DefineVertices();
            fixed (float* v = &m_vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * m_vertices.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(0, 2, GL_FLOAT, false, 2 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            //Colors buffer object
            glBindBuffer(GL_ARRAY_BUFFER, m_vboColors);

            m_vertexColors = DefineVertexColors();
            fixed (float* v = &m_vertexColors[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * m_vertexColors.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(1, 3, GL_FLOAT, false, 3 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(1);


            //Unbind
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            Loaded = true;
        }

        /// <summary>
        /// Unloads shape vertices from memory.
        /// </summary>
        public void Unload()
        {
            glDeleteBuffer(m_vboVertices);
            glDeleteBuffer(m_vboColors);
            glDeleteVertexArray(m_vao);

            Loaded = false;
        }


        /// <summary>
        /// Sets a new color for a shape. This will reload the shape if required.
        /// Custom shapes will ignore this color.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            m_color = color;

            if (Loaded)
            {
                Unload();
                Load();
            }
        }

        /// <summary>
        /// Sets the current animation of this shape.
        /// </summary>
        /// <param name="animation"></param>
        public void SetAnimation(Animation animation)
        {
            Animation = animation;
        }

        /// <summary>
        /// Updates animation if one exists.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Animate(float deltaTime)
        {
            Animation?.UpdateAnimation(Transform, deltaTime);
        }

        /// <summary>
        /// Renders shape to a given camera.
        /// </summary>
        /// <param name="camera"></param>
        public void Render(Camera2D camera)
        {
            m_shader.SetMatrix4x4("model", Transform.CreateModelMatrix());
            m_shader.Use();
            m_shader.SetMatrix4x4("projection", camera.ProjectionMatrix);

            glBindVertexArray(m_vao); //Bind
            glDrawArrays(GL_TRIANGLES, 0, m_vertices.Length); //Draw
            glBindVertexArray(0); //Unbind
        }


    }
}
