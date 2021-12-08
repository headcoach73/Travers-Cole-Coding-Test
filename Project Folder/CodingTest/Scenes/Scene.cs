using System;
using System.Collections.Generic;
using CodingTest.ApplicationLoop;
using CodingTest.Rendering.Cameras;
using CodingTest.Rendering.Display;
using CodingTest.Shapes;

namespace CodingTest.Scenes
{
    class Scene
    {
        /// <summary>
        /// List of current shapes in the scene. Modifiying this list doesn't effect whats in the scene, this is just a copy.
        /// </summary>
        public List<Shape> SceneShapes => new List<Shape>(m_sceneShapes.Values);

        /// <summary>
        /// Camera to used to render the scene.
        /// </summary>
        public Camera2D SceneCamera { get; set; }

        private Dictionary<string, Shape> m_sceneShapes = new Dictionary<string, Shape>();
        private int idNumber = 0;

        public Scene()
        {
            SceneCamera = new Camera2D(DisplayManager.WindowSize / 2f, 1);
        }

        public void LoadScene()
        {
            OnSceneLoad();
        }

        /// <summary>
        /// Callback for the scene load. Called before the scene is rendered.
        /// </summary>
        protected virtual void OnSceneLoad()
        {

        }

        /// <summary>
        /// Adds shape to the scene to be rendered.
        /// </summary>
        /// <param name="shape"></param>
        public void AddShapeToScene(Shape shape)
        {
            if (!shape.Loaded) shape.Load();
            idNumber++;
            shape.ShapeID = $"{shape.ShapeType.ToLower()}_{idNumber}";
            m_sceneShapes[shape.ShapeID] = shape;
        }

        /// <summary>
        /// Removes shape from scene.
        /// </summary>
        /// <param name="shape"></param>
        public void RemoveShapeFromScene(Shape shape)
        {
            if (shape.Loaded) shape.Unload();
            m_sceneShapes.Remove(shape.ShapeID);
        }

        /// <summary>
        /// Get shape in scene by ID
        /// </summary>
        /// <param name="shapeID"></param>
        /// <returns></returns>
        public Shape GetShapeByID(string shapeID)
        {
            if (!m_sceneShapes.ContainsKey(shapeID)) return null;

            return m_sceneShapes[shapeID];
        }

        /// <summary>
        /// Renders all shapes in the scene.
        /// </summary>
        public void RenderScene()
        {
            SceneCamera.UpdateProjectionMatrix();

            foreach (var shape in m_sceneShapes.Values)
            {
                if (shape.Loaded) shape.Render(SceneCamera);
            }
        }

        /// <summary>
        /// Updates scene and updates scene shapes animations.
        /// </summary>
        public void UpdateScene()
        {
            OnSceneUpdate();
            UpdateAnimations();
        }

        /// <summary>
        /// Scene update callback. Called before animations are updated.
        /// </summary>
        protected virtual void OnSceneUpdate()
        {

        }

        private void UpdateAnimations()
        {
            foreach (var shape in m_sceneShapes.Values)
            {
                shape.Animate(Time.DeltaTime);
            }
        }
    }
}
