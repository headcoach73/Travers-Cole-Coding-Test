using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTest.ApplicationLoop
{
    static class Time
    {
        /// <summary>
        /// Time since last frame.
        /// </summary>
        public static float DeltaTime { get; set; }

        /// <summary>
        /// Total time since program started.
        /// </summary>
        public static float TotalElapsedSeconds { get; set; }
    }
}
