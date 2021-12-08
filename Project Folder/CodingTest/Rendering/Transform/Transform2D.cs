using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CodingTest.Rendering.Transform
{
    class Transform2D : Transform
    {
        /// <summary>
        /// 2D position of object in the scene
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// 2D scale of object in the scene.
        /// </summary>
        public Vector2 Scale { get; set; }
        /// <summary>
        /// 2D rotation of an object in the scene. Rotation axis is the z axis coming out of the screen.
        /// </summary>
        public float Rotation { get; set; }

        public Transform2D(Vector2 position, Vector2 scale, float rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public Transform2D()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0;
        }

        public override Matrix4x4 CreateModelMatrix()
        {
            Matrix4x4 translationMat = Matrix4x4.CreateTranslation(Position.X, Position.Y, 0);
            Matrix4x4 scaleMat = Matrix4x4.CreateScale(Scale.X, Scale.Y, 1);
            Matrix4x4 rotationMat = Matrix4x4.CreateRotationZ(Rotation);

            return scaleMat * rotationMat * translationMat;
        }
    }
}
