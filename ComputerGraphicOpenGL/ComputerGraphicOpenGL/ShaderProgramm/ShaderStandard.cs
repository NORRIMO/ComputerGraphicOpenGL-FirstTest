using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace ComputerGraphicOpenGL.ShaderProgramm
{
    public static class ShaderStandard
    {
        private static int shaderID = -1;

        private static int vertexShaderID = -1;
        private static int fragmentShaderID = -1;

        private static int uniformMatrix = -1;
        private static int uniformTexture = -1;

        public static void Init()
        {
            shaderID = GL.CreateProgram();

            Assembly assembly = Assembly.GetExecutingAssembly();
            
            //vertex shader auslesen:
            Stream streamVertex = assembly.GetManifestResourceStream("ComputerGraphicOpenGL.ShaderProgramm.shaderStandard_vertex.glsl");
            StreamReader streamReaderVertex = new StreamReader(streamVertex);
            string stringVertexCode = streamReaderVertex.ReadToEnd();
            streamReaderVertex.Dispose();
            streamVertex.Close();

            //fragment shader auslesen:
            Stream streamFragment = assembly.GetManifestResourceStream("ComputerGraphicOpenGL.ShaderProgramm.shaderStandard_fragment.glsl");
            StreamReader streamReaderFragment = new StreamReader(streamFragment);
            string stringFragmentCode = streamReaderFragment.ReadToEnd();
            streamReaderFragment.Dispose();
            streamFragment.Close();

            vertexShaderID = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderID, stringVertexCode);

            fragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderID, stringFragmentCode);

            GL.CompileShader(vertexShaderID);
            GL.AttachShader(shaderID, vertexShaderID);

            GL.CompileShader(fragmentShaderID);
            GL.AttachShader(shaderID, fragmentShaderID);

            GL.LinkProgram(shaderID);

            uniformMatrix = GL.GetUniformLocation(shaderID, "uniformMatrix");
            uniformTexture = GL.GetUniformLocation(shaderID, "uTexture");

        }

        public static int GetShaderID()
        {
            return shaderID;
        }

        public static int GetMatrixID()
        {
            return uniformMatrix;
        }

        public static int GetTextureID()
        {
            return uniformTexture;
        }
    }
}
