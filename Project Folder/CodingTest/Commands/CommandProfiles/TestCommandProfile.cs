using CodingTest.Animations;
using CodingTest.Rendering.Display;
using CodingTest.Rendering.Shaders;
using CodingTest.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace CodingTest.Commands.CommandProfiles
{
    class TestCommandProfile : CommandProfile
    {
        public override List<CommandBase> LoadCommands()
        {
            var Singleton = TestApplication.Singleton;

            var spawnCommand = new Command<string>("spawn", "Spawn a given shape in a random location and color on the screen", "spawn <shapetype>", (string shapeType) =>
            {
                shapeType = shapeType.ToLower();
                Random random = new Random();


                Shape shape;
                Color randomColor = Color.FromArgb(random.Next(200), random.Next(256), random.Next(256));

                if (shapeType == "triangle")
                {
                    shape = new Triangle(ShaderManager.DefaultShader, randomColor, 100, 100);
                }
                else if (shapeType == "circle")
                {
                    shape = new Circle(ShaderManager.DefaultShader, randomColor, 100, 50);
                }
                else if (shapeType == "hexagon")
                {
                    shape = new Hexagon(ShaderManager.DefaultShader, randomColor, 100);
                }
                else if (shapeType == "rectangle")
                {
                    shape = new Shapes.Rectangle(ShaderManager.DefaultShader, randomColor, 100, 50);
                }
                else
                {
                    Console.WriteLine("Invalid shape type, Valid shape types: Triangle, Circle, Hexagon, Rectangle");
                    return;
                }

                Vector2 randomScreenPosition = new Vector2(random.Next(0, (int)DisplayManager.WindowSize.X), random.Next(0, (int)DisplayManager.WindowSize.Y));
                shape.Transform.Position = randomScreenPosition;
                Singleton.Scene.AddShapeToScene(shape);
                Console.WriteLine($"{shape.ShapeType} spawned at screen position x:{shape.Transform.Position.X}, y:{shape.Transform.Position.Y}");
            });

            var spawnCustomShape = new Command<string>("spawn_custom", "Spawn a custom shape from a textfile in a random location on the screen", "spawn_custom <filepath>", (string filePath) =>
            {
                Random random = new Random();

                Shape shape = ShapeLoader.LoadShape(filePath, ShaderManager.DefaultShader);

                if (shape == null)
                {
                    Console.WriteLine("Invalid filepath... could not spawn custom shape");
                    return;
                }

                Vector2 randomScreenPosition = new Vector2(random.Next(0, (int)DisplayManager.WindowSize.X), random.Next(0, (int)DisplayManager.WindowSize.Y));
                shape.Transform.Position = randomScreenPosition;
                Singleton.Scene.AddShapeToScene(shape);
                Console.WriteLine($"{shape.ShapeType} spawned at screen position x:{shape.Transform.Position.X}, y:{shape.Transform.Position.Y}");
            });

            var listShapes = new Command("shape_list", "Shows a list of current shapes", "shape_list", () =>
            {
                var sceneShapes = Singleton.Scene.SceneShapes;
                for (int i = 0; i < sceneShapes.Count; i++)
                {
                    Console.WriteLine($"[{i}]: ID: {sceneShapes[i].ShapeID}");
                }
            });

            var shapeProperties = new Command<string>("properties", "Shows the properties of a given shape. Use shape_list for a list of shapes", "properties <shape_id>", (string shapeID) =>
            {
                var shape = Singleton.Scene.GetShapeByID(shapeID);

                if (shape == null)
                {
                    Console.WriteLine("Incorrect shapeID, to see a list of shapes use shape_list");
                    return;
                }

                Console.WriteLine($"ID: {shapeID} Type: {shape.ShapeType}");
                Console.WriteLine($"Position: {shape.Transform.Position} Rotation: {shape.Transform.Rotation} Scale: {shape.Transform.Scale}");
                Console.WriteLine($"Color: R:{shape.Color.R} G:{shape.Color.G} B:{shape.Color.B}");
                Console.WriteLine($"Animation: {shape.Animation.AnimationType}");
            });

            var helpCommand = new Command("help", "shows a list of commands", "help", CommandManager.ShowCommands);

            var setPosition = new ShapeCommand<Vector2>("set_position", "Sets the position of the given shape", "set_position <shapeID> <xValue> <yValue>", (string shapeID, Vector2 position) =>
            {
                Shape shape = Singleton.Scene.GetShapeByID(shapeID);

                if (shape == null)
                {
                    Console.WriteLine("Invalid shape id, use shape_list for list of shape ids.");
                    return;
                }

                shape.Transform.Position = position;
                Console.WriteLine($"{shape.ShapeID} position set to {position}");
            });

            var setRotation = new ShapeCommand<float>("set_rotation", "Sets the rotation of the given shape", "set_rotation <shapeID> <radians>", (string shapeID, float rotation) =>
            {
                Shape shape = Singleton.Scene.GetShapeByID(shapeID);

                if (shape == null)
                {
                    Console.WriteLine("Invalid shape id, use shape_list for list of shape ids.");
                    return;
                }

                shape.Transform.Rotation = rotation;
                Console.WriteLine($"{shape.ShapeID} rotation set to {rotation}");
            });

            var setScale = new ShapeCommand<Vector2>("set_scale", "Sets the scale of the given shape", "set_scale <shapeID> <xValue> <yValue>", (string shapeID, Vector2 scale) =>
            {
                Shape shape = Singleton.Scene.GetShapeByID(shapeID);

                if (shape == null)
                {
                    Console.WriteLine("Invalid shape id, use shape_list for list of shape ids.");
                    return;
                }

                shape.Transform.Scale = scale;
                Console.WriteLine($"{shape.ShapeID} scale set to {scale}");
            });

            var setColor = new ShapeCommand<Color>("set_color", "Sets the color of the given shape", "set_color <shapeID> <r> <g> <b>", (string shapeID, Color color) =>
            {
                Shape shape = Singleton.Scene.GetShapeByID(shapeID);

                if (shape == null)
                {
                    Console.WriteLine("Invalid shape id, use shape_list for list of shape ids.");
                    return;
                }

                shape.SetColor(color);
                Console.WriteLine($"{shape.ShapeID} color set to {color}");
            });

            var setAnimation = new ShapeCommand<string>("set_animation", "Sets the animation of a given shape", "set_animation <shapeID> <animationtype>", (string shapeID, string animationType) =>
            {
                Shape shape = Singleton.Scene.GetShapeByID(shapeID);

                if (shape == null)
                {
                    Console.WriteLine("Invalid shape id, use shape_list for list of shape ids.");
                    return;
                }

                animationType = animationType.ToLower();

                if (animationType == "box")
                {
                    shape.SetAnimation(new BoxAnimation(new Vector2(100, 100), new Vector2(400, 400), 50));
                }
                else if(animationType == "circle") 
                {
                    shape.SetAnimation(new CircleAnimation(new Vector2(300, 300), 50, 2));
                } 
                else if (animationType == "horizontal")
                {
                    shape.SetAnimation(new HorizontalAnimation(50));

                }
                else if (animationType == "vertical")
                {
                    shape.SetAnimation(new VerticalAnimation(50));
                }
                else
                {
                    Console.WriteLine("Invalid animation type, Valid shape types: Box, Circle, Horizontal, Vertical");
                    return;
                }
                    
                Console.WriteLine($"{shape.ShapeID} animtation set to {shape.Animation.AnimationType}");
            });

            var setCustomAnimation = new ShapeCommand<string>("set_custom_animation", "Set custom animation from a textfile for a given shape", "set_custom_animation <shapeID> <filename>", (string shapeID, string filename) =>
            {
                Shape shape = Singleton.Scene.GetShapeByID(shapeID);

                if (shape == null)
                {
                    Console.WriteLine("Invalid shape id, use shape_list for list of shape IDs.");
                    return;
                }

                CustomAnimation animation = AnimationLoader.LoadAnimation(filename);

                if (animation == null)
                {
                    Console.WriteLine("Invalid filepath... could not load custom animation.");
                    return;
                }

                shape.SetAnimation(animation);
                Console.WriteLine($"{shape.ShapeID} animtation set to {shape.Animation.AnimationType}.");
            });

            var commandList = new List<CommandBase>
            {
                spawnCommand,
                spawnCustomShape,
                listShapes,
                shapeProperties,
                setPosition,
                setScale,
                setRotation,
                setColor,
                setAnimation,
                setCustomAnimation,
                helpCommand,

            };

            return commandList;
        }
    }
}
