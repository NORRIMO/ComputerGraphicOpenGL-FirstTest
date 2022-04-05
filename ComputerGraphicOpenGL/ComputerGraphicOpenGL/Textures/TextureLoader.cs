using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;

namespace ComputerGraphicOpenGL.Textures
{
    public static class TextureLoader
    {
        //um die bewreits verwendeten texturen abzuspeichern, damit man nicht immer neuen speicher verbraucht wenn man eine textur läd
        private static Dictionary<string, int> textureDictionary = new Dictionary<string, int>();

        public static void RegisterTexture(string filename, int openglID)
        {
            if (textureDictionary.ContainsKey(filename))
            {
                throw new Exception("Texture is already stored in dictionary.");
            }
            else
            {
                textureDictionary.Add(filename, openglID);

            }
        }

        public static bool IsTextureAlreadyDefined(string filename)
        {
            return textureDictionary.ContainsKey(filename);
        }

        public static int GetTexture(string filename)
        {
            textureDictionary.TryGetValue(filename, out int ID);
            return ID;
        }

        public static int LoadTexture(string file)
        {
            if (IsTextureAlreadyDefined(file))  
            {
                throw new Exception("Texture is already defined.");
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(file);

            Bitmap bitmap = new Bitmap(stream);
            if (bitmap == null)
            {
                return -1; //fehlerfall
            }
            else
            {
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                                        ImageLockMode.ReadOnly, 
                                        bitmap.PixelFormat);

                int textureID = GL.GenTexture();

                GL.BindTexture(TextureTarget.Texture2D, textureID);

                GL.TexImage2D(TextureTarget.Texture2D, 0, 
                    bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ? PixelInternalFormat.Rgb8 : PixelInternalFormat.Rgba8, 
                    bitmap.Width, bitmap.Height, 0,
                    bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ? OpenTK.Graphics.OpenGL4.PixelFormat.Bgr : OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,  
                    PixelType.UnsignedByte, bitmapData.Scan0);

                //sorgt dafür dass texturen nicht so schnell matschig/perpektivisch verzerrt werden wenn man sie aus einem bestimmten winkel betrachtet
                GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)OpenTK.Graphics.OpenGL.ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, 8);
               
                //mipmaps sind verschiedene versionen der textur, wenn das object mit der textur sehr klein zu sehen ist dann sieht man auch nur eine verkleinerte version der textur
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                //ist das selbe nur wenns größer dargestellt wird als es ist
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                //Texture wrap, S und T sind axen, repeat heißt die textur wird wiederholt wenns sie größer als 1 ist
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, 0);
               
                bitmap.Dispose();
                stream.Close();

                RegisterTexture(file, textureID);

                return textureID;
            }
        }
    }
}
