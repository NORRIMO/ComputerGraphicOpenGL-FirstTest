using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphicOpenGL.Textures;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ComputerGraphicOpenGL.GameCore
{
    public class GameObject
    {
        public Vector3 Position = new Vector3(0, 0, 0); 
        private Quaternion Rotation = new Quaternion(0, 0, 0, 1);
        private Vector3 Scale = new Vector3(1, 1, 1);
        private int textureID = -1;

        public int GetTextureID()
        {           
            return textureID;
        }

        public void SetTexture(string filename)
        {
            if (TextureLoader.IsTextureAlreadyDefined(filename))
            {
                textureID = TextureLoader.GetTexture(filename);
            }
            else
            {
                textureID = TextureLoader.LoadTexture(filename);
            }
        }



        public void SetScale(float x, float y, float z)
        {
            if (x > 0 && y > 0 && z > 0)
            {
                Scale = new Vector3(x, y, z);
            }
            else
            {
                //fehelerfall: exeption oder fehlermeldung ausgeben
                Scale = new Vector3(1, 1, 1);
            }
        }

        public Vector3 GetScale()
        {
            return Scale;
        }

        //rotation um eine axis
        public void AddRotation(Axis a, float degrees)
        {
            Quaternion newRotation;
            if (a == Axis.X)
            {
                newRotation = Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(degrees));
            }

            else if(a == Axis.Y)
            {
                newRotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(degrees));
            }

            else 
            {
                newRotation = Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(degrees));
            }

            Rotation = Rotation * newRotation;

        }

        public Quaternion GetRotation()
        {
            return Rotation;
        }

        public void Update(KeyboardState input_K, MouseState input_M)
        {
            if (input_K[Keys.W] || input_M.Y > 590)
            {
                Position = new Vector3(Position.X, Position.Y + 2, Position.Z);     
            } 
            
            if (input_K[Keys.S] || input_M.Y < 10)
            {
                Position = new Vector3(Position.X, Position.Y - 2, Position.Z);
            } 

            if (input_K[Keys.A] || input_M.X > 590)
            {
                Position = new Vector3(Position.X - 2, Position.Y , Position.Z);
            } 
            
            if (input_K[Keys.D] || input_M.X < 10)
            {
                Position = new Vector3(Position.X + 2, Position.Y, Position.Z);
            }

           
        }

    }
}
