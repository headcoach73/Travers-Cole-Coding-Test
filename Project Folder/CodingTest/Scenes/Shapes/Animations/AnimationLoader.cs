using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace CodingTest.Animations
{
    static class AnimationLoader
    {
        private const string POSITIONS_START = "#Positions:";
        private const string MOVEMENT_RATE_START = "#MovementRate:";

        /// <summary>
        /// Loads a custom animation from a textfile
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>
        /// returns null if filename couldn't be read.
        /// </returns>
        public static CustomAnimation LoadAnimation(string fileName)
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

            List<Vector2> positions = new List<Vector2>();
            float movementRate = 50f;
            int index = 0;
            while (index < lines.Length)
            {
                if (lines[index] == POSITIONS_START)
                {
                    positions = ReadVectors(lines, index + 1);
                }
                else if (lines[index] == MOVEMENT_RATE_START)
                {
                    movementRate = ReadFloat(lines[index + 1]);
                }

                index++;
            }

            return new CustomAnimation(positions, movementRate);
        }

        private static List<Vector2> ReadVectors(string[] lines, int startingIndex)
        {
            List<Vector2> vectors = new List<Vector2>();
            int currentIndex = startingIndex;

            while (currentIndex < lines.Length && lines[currentIndex][0] != '#')
            {
                var stringValues = lines[currentIndex].Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (stringValues.Length == 2)
                {
                    float xvalue;
                    bool xparseSuccess = float.TryParse(stringValues[0].Trim(), out xvalue);
                    float yvalue;
                    bool yparseSuccess = float.TryParse(stringValues[1].Trim(), out yvalue);

                    if (xparseSuccess && yparseSuccess)
                    {
                        vectors.Add(new Vector2(xvalue, yvalue));
                    }
                }

                currentIndex++;
            }
            return vectors;
        }

        private static float ReadFloat(string line)
        {
            string trimmedLine = line.Trim();

            float value;
            bool parseSuccess = float.TryParse(trimmedLine, out value);

            if (!parseSuccess)
            {
                Console.WriteLine("Movement rate parse failed");
            }

            return value;
        }

        public static void SaveAnimation(string fileName, CustomAnimation animation)
        {
            string animationInfo = "";
            animationInfo += POSITIONS_START + "\n";
            animationInfo += FormatVector2Sequence(animation.PositionSequence);
            animationInfo += MOVEMENT_RATE_START + "\n";
            animationInfo += animation.MovementRate.ToString();
            File.WriteAllText(fileName, animationInfo);
        }

        private static string FormatVector2Sequence(List<Vector2> vectors)
        {
            string info = "";
            foreach (var vector in vectors)
            {
                info += $"{vector.X}, {vector.Y}\n";
            }
            return info;
        }
    }
}
