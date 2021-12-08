using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTest.Rendering.Shaders
{
    static class ShaderManager
    {
        /// <summary>
        /// Default shader for rendering.
        /// </summary>
        public static Shader DefaultShader { get; private set; }

        /// <summary>
        /// Initialize ShaderManager by loading and compiling the default shader.
        /// </summary>
        public static void Initialize()
        {
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    out vec4 vertexColor;
                                    
                                    uniform mat4 projection;
                                    uniform mat4 model;

                                    void main()
                                    {
                                        vertexColor = vec4(aColor.r/255.0, aColor.g/255.0, aColor.b/255.0, 1.0);
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                    }";

            string fragmentShader = @"#version 330 core
                                        out vec4 FragColor;
                                        in vec4 vertexColor;
                                        
                                        void main()
                                        {
                                            FragColor = vertexColor;
                                        }";

            DefaultShader = new Shader(vertexShader, fragmentShader);
            DefaultShader.Load();
        }
    }
}
