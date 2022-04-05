using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace ComputerGraphicOpenGL.Primitives
{
    public static class PrimitiveQuad
    {
        private static int vertexArrayObject = -1; //alle werte kleiner 0 sind in openGL fehlerwerte

        private static float[] verticies = new float[]
        {
            //erstes dreieck
            -0.5f, -0.5f, 0,  //A x,y,z
            +0.5f, -0.5f, 0,  //B
            -0.5f, +0.5f, 0,  //C

            //zweites dreieck
            +0.5f, -0.5f, 0,  //B
            +0.5f, +0.5f, 0,  //D
            -0.5f, +0.5f, 0   //C
        };


        private static float[] uvs = new float[]
        {
            0, 1,           //A
            1, 1,           //B
            0, 0,           //C

            1, 1,           //B
            1, 0,           //D
            0, 0            //C

        };


        public static void Init()
        {
            vertexArrayObject = GL.GenVertexArray(); //vertexArrayObject gibt einen int (id) wieder der die adresse vom generierten vertex array darstellt
            GL.BindVertexArray(vertexArrayObject);

            //buffer für vertiecies
            int vertexBufferObjectVerticies = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjectVerticies);
            GL.BufferData(BufferTarget.ArrayBuffer, verticies.Length * 4, verticies, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //0 ist reserviert für ein ungültiges objekt, damit wird signaliesiert das die folgenden befehle nicht mehr für diese objekt gelten

            //buffer für texturkoordinaten
            int vertexBufferObjectUVs = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjectUVs);
            GL.BufferData(BufferTarget.ArrayBuffer, uvs.Length * 4, uvs, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0); //0 ist reserviert für ein ungültiges objekt, damit wird signaliesiert das die folgenden befehle nicht mehr für diese objekt gelten
        }

        public static int GetVertexArrayObjectID()
        {
            return vertexArrayObject;
        }

        public static int GetPointCount()
        {
            return 6;
        }
    }
}
