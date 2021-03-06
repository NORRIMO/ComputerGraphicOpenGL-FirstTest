using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace ComputerGraphicOpenGL.Primitives
{
    public static class PrimitiveTriangle
    {
        private static int vertexArrayObject = -1; //alle werte kleiner 0 sind in openGL fehlerwerte

        private static float[] verticies = new float[]
        {
            -0.5f, -0.5f, 0,    //A  formt einen Punkt, 1.punkt
            +0.5f, -0.5f, 0,    //B                     2.punkt
            +0.0f, +0.5f, 0     //C                     3.punkt
            //9 float werte die je 4 bytes verbrauchen
        };


        private static float[] uvs = new float[]
    {
           0, 1,      // 1.punkt (texturkoordinate)
           1, 1,      // 2.punkt (texturkoordinate)
           0.5f, 0    // 3.punkt (texturkoordinate)
    };


        public static void Init()
        {
            vertexArrayObject = GL.GenVertexArray(); //vertexArrayObject gibt einen int (id) wieder der die adresse vom generierten vertex array darstellt
            GL.BindVertexArray(vertexArrayObject);

            //buffer für vertiecies
            int vertexBufferObjectVerticies = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjectVerticies);
            GL.BufferData(BufferTarget.ArrayBuffer, 9 * 4, verticies, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //0 ist reserviert für ein ungültiges objekt, damit wird signaliesiert das die folgenden befehle nicht mehr für diese objekt gelten

            //buffer für texturkoordinaten
            int vertexBufferObjectUVs = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjectUVs);
            GL.BufferData(BufferTarget.ArrayBuffer, 6 * 4, uvs, BufferUsageHint.StaticDraw);
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
            return 3;
        }
    }
}
