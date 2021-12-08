using CodingTest.Animations;
using CodingTest.Rendering.Shaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodingTest.Shapes
{
    static class ShapeLoader
    {
        const string VERTICES_START = "#Vertices:";
        const string VERTEX_COLORS_START = "#VertexColors:";

        /// <summary>
        /// Loads a custom shape from a text file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="shader"></param>
        /// <returns>
        /// Returns null if filename could not be read.
        /// </returns>
        public static CustomShape LoadShape(string fileName, Shader shader)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(fileName);
            }
            catch
            {
                return null;
            }

            float[] vertices = { };
            float[] vertexColors = { };
 
            int index = 0;
            while (index < lines.Length)
            {
                if (lines[index] == VERTICES_START)
                {
                    vertices = ReadVertices(lines, index + 1);
                }
                else if (lines[index] == VERTEX_COLORS_START)
                {
                    vertexColors = ReadVertices(lines, index + 1);
                }

                index++;
            }

            return new CustomShape(shader, vertices, vertexColors);
        }

        private static float[] ReadVertices(string[] lines, int startingIndex)
        {
            List<float> vertices = new List<float>();
            int currentIndex = startingIndex;

            while (currentIndex < lines.Length && lines[currentIndex][0] != '#')
            {
                var stringValues = lines[currentIndex].Split(',', StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < stringValues.Length; i++)
                {
                    float value;
                    bool parseSuccess = float.TryParse(stringValues[i].Trim(), out value);
                    if (parseSuccess) 
                    {
                        vertices.Add(value);
                    } 
                }

                currentIndex++;
            }
            return vertices.ToArray();
        }


        /// <summary>
        /// Saves a shape to a text file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="shape"></param>
        public static void SaveShape(string fileName, Shape shape)
        {
            string shapeInfo = "";
            shapeInfo += "#Vertices:\n";
            shapeInfo += FormatVertexStrings(shape.DefineVertices(), 2);
            shapeInfo += "#VertexColors:\n";
            shapeInfo += FormatVertexStrings(shape.DefineVertexColors(), 3);


            File.WriteAllText(fileName, shapeInfo);
        }

        private static string FormatVertexStrings(float[] vertices, int pointsPerVertex)
        {
            string verticesInfo = "";

            for (int i = 0; i < vertices.Length; i += pointsPerVertex)
            {
                for (int j = 0; j < pointsPerVertex; j++)
                {
                    verticesInfo += $"{vertices[i + j]}, ";
                }
                verticesInfo += "\n";
            }

            return verticesInfo;
        }
    }
}
