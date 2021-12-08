using CodingTest.ApplicationLoop;
using CodingTest.Rendering.Display;
using CodingTest.Rendering.Shaders;
using GLFW;
using CodingTest.Shapes;
using static CodingTest.OpenGL.GL;
using CodingTest.Rendering.Cameras;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using CodingTest.Animations;
using CodingTest.Scenes;
using CodingTest.Commands;
using CodingTest.Commands.CommandProfiles;

namespace CodingTest
{
    class TestApplication : Application
    {
        public static TestApplication Singleton;
        public Scene Scene => m_scene;

        private Scene m_scene;

        public TestApplication(int initialWindowWidth, int initialWindowHeight, string intialWindowTitle) : base(initialWindowWidth, initialWindowHeight, intialWindowTitle)
        {
        }

        protected override void Initialize()
        {
            //Singleton so commands can reference the scene.
            Singleton = this;

            //Init command line manager with commands from test command profile
            CommandManager.Initialize(new TestCommandProfile());
        }

        protected override void LoadContent()
        {
            //Init default shader
            ShaderManager.Initialize();

            //Create test scene
            m_scene = new TestScene();

            //Load test scene content
            m_scene.LoadScene();
        }
        protected override void Update()
        {
            //Handle commands
            CommandManager.HandleCommandQueue();

            //Update scene
            m_scene.UpdateScene();
        }


        protected override void Render()
        {
            //Clear the screen
            glClearColor(0,0,0,0);
            glClear(GL_COLOR_BUFFER_BIT);

            //Render the current scene
            m_scene.RenderScene();
             
            Glfw.SwapBuffers(DisplayManager.Window);
        }

    }
}
