using System;
using System.Numerics;


namespace CodingTest.Rendering.Transform
{
    abstract class Transform
    {
        /// <summary>
        /// Creates the model matrix to define an objects position in the scene.
        /// </summary>
        /// <returns></returns>
        public abstract Matrix4x4 CreateModelMatrix();
    }
}
