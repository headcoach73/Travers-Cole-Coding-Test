using CodingTest.Animations;
using CodingTest.ApplicationLoop;
using CodingTest.Rendering.Shaders;
using CodingTest.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace CodingTest.Scenes
{
    class TestScene : Scene
    {
        protected override void OnSceneLoad()
        {
            var shader = ShaderManager.DefaultShader; 

            //Populate test scene with some shapes and animations

            Vector2 position = new Vector2(400, 300);
            float rotation = MathF.PI / 4f;
            var rectangle = new Shapes.Rectangle(shader, Color.Blue, 150, 100);
            rectangle.Transform.Position = position;
            rectangle.Transform.Rotation = rotation;
            AddShapeToScene(rectangle);

            var hexagon = new Hexagon(shader, Color.Yellow, 50);
            hexagon.Transform.Position = new Vector2(50, 100);
            AddShapeToScene(hexagon);

            var circle = new Circle(shader, Color.Red, 100, 50);
            circle.Transform.Position = new Vector2(600, 400);
            AddShapeToScene(circle);

            var triangle = new Triangle(shader, Color.Aquamarine, 70, 50);
            triangle.Transform.Position = new Vector2(300, 300);
            AddShapeToScene(triangle);

            var triangle2 = ShapeLoader.LoadShape("Triangle.txt", shader);
            triangle2.Transform.Position = new Vector2(300, 300);
            AddShapeToScene(triangle2);

            triangle.SetAnimation(new HorizontalAnimation(150));
            var animation = new VerticalAnimation(100);
            rectangle.SetAnimation(animation);
            circle.SetAnimation(new BoxAnimation(new Vector2(100, 100), new Vector2(500, 500), 100));
            hexagon.SetAnimation(new CircleAnimation(new Vector2(500, 400), 250, 1));
        }

        protected override void OnSceneUpdate()
        {
            //Just spins the objects in the scene as a test
            
            //foreach (var shape in SceneShapes)
            //{
            //    shape.Transform.Rotation = MathF.Cos(Time.TotalElapsedSeconds) * MathF.PI * 2f;
            //}
        }
    }
}
