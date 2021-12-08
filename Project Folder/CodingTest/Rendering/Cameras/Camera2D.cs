using CodingTest.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace CodingTest.Rendering.Cameras
{
    class Camera2D
    {
        /// <summary>
        /// Current ProjectionMatrix used to render objects to this camera.
        /// </summary>
        public Matrix4x4 ProjectionMatrix { get; private set; }
        /// <summary>
        /// Focus position for camera.
        /// </summary>
        public Vector2 FocusPosition { get; set; }
        /// <summary>
        /// Zoom scale for camera
        /// </summary>
        public float Zoom { get; set; }
        public Camera2D(Vector2 focusPosition, float zoom)
        {
            FocusPosition = focusPosition;
            Zoom = zoom;
        }

        /// <summary>
        /// Updates the camera projection matrix, this needs to be updated everytime FocusPosition or Zoom is changed.
        /// </summary>
        public void UpdateProjectionMatrix()
        {
            float left = FocusPosition.X - DisplayManager.WindowSize.X / 2f;
            float right = FocusPosition.X + DisplayManager.WindowSize.X / 2f;
            float top = FocusPosition.Y - DisplayManager.WindowSize.Y / 2f;
            float bottom = FocusPosition.Y + DisplayManager.WindowSize.Y / 2f;

            Matrix4x4 orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100f);
            Matrix4x4 zoomMatrix = Matrix4x4.CreateScale(Zoom);

            ProjectionMatrix = orthoMatrix * zoomMatrix;
        }
    }
}
