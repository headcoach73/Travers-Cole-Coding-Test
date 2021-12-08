using CodingTest.Rendering.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CodingTest.Shapes
{
    class Hexagon : Circle
    {
        //Hexagon can be made with the same alogrithim as a circle just with a triangle count of 6
        public Hexagon(Shader shader, Color color, float radius) : base(shader, color, radius, 6)
        {


        }
    }
}
