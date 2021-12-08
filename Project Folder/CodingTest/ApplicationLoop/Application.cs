using CodingTest.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Text;
using GLFW;

namespace CodingTest.ApplicationLoop
{
    abstract class Application
    {
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string IntialWindowTitle { get; set; }
        public Application(int initialWindowWidth, int initialWindowHeight, string intialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            IntialWindowTitle = intialWindowTitle;
        }

        public void Run()
        {
            Initialize();

            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, IntialWindowTitle);

            LoadContent();

            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                Time.DeltaTime = (float)Glfw.Time - Time.TotalElapsedSeconds;
                Time.TotalElapsedSeconds = (float)Glfw.Time;

                Update();

                Glfw.PollEvents();

                Render();
            }

            DisplayManager.CloseWindow();
        }

        /// <summary>
        /// Initialize prior to window creation
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Load content for rendering after window created
        /// </summary>
        protected abstract void LoadContent();

        /// <summary>
        /// Called every frame before render update
        /// </summary>
        protected abstract void Update();

        /// <summary>
        /// Render every frame after update
        /// </summary>
        protected abstract void Render();

    }
}
