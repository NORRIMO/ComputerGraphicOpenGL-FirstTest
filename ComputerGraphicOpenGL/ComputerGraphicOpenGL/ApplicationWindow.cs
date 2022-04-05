using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using ComputerGraphicOpenGL.Primitives;
using ComputerGraphicOpenGL.ShaderProgramm;
using ComputerGraphicOpenGL.Textures;
using ComputerGraphicOpenGL.GameCore;
using System.Diagnostics;

namespace ComputerGraphicOpenGL
{
    class ApplicationWindow : GameWindow
    {

        private GameWorld currentWorld = new GameWorld();

        private Matrix4 projectionMatrix = Matrix4.Identity;    //gleicht das bildschirmverhältnis (zb 16:9) aus
        private Matrix4 viewMatrix = Matrix4.Identity;          //simuliert kamera

        private int textureExample = -1;

        public ApplicationWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
            : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            //wird ausgeführt wenn das fenster tatsächlich dargestellt/aufgebaut wird
            //basis openGL aktionen (zb grundlegende einstellungen)

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1); //farbe des gelöschten bildschirms wählen
            
            GL.Enable(EnableCap.DepthTest); //deaphbuffer aktivieren, dephtest sorgt dafür das teile eines objektes die hinter anderen objekten liegen nicht mehr dargestellt werden
          
            GL.Enable(EnableCap.CullFace); //faceculling, wenn objekt nicht im fenster zu sehen ist, dann wird es auch nicht gerendert zB rückseite eines würfels die man nicht sehen würde wird nicht gerendert
            GL.CullFace(CullFaceMode.Back); //der kamera abgewandte flächen werden ignoriert
            GL.FrontFace(FrontFaceDirection.Ccw); //definiert wie faces gezeichnet werden, Ccw - counter clock wise

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0); //framebuffer sind bilder die im hintergund im arbeitsspeicher abgelegt werden und alles was per draw befehl gezeichnet wird geht an den monitor -> 0
                                                                  //framebuffer hat immer die größe des bildschirms, wenn die anwendung im fenster modus ausgeführt, dann hat er die größe des fensters
            PrimitiveTriangle.Init();
            PrimitiveQuad.Init();
            ShaderStandard.Init();

            viewMatrix = Matrix4.LookAt((0, 0, 1), (0, 0, 0), (0, 1, 0));

            
            GameObject g1 = new GameObject();
            g1.Position = new Vector3(-300, 100, 0);
            g1.SetScale(100, 100, 1);
            g1.SetTexture("ComputerGraphicOpenGL.Textures.starTransparentTexture.jpg");
            currentWorld.AddGameObject(g1);

            /*
            GameObject g2 = new GameObject();
            g2.Position = new Vector3(0, 0, 0);
            g2.SetScale(100, 100, 1);
            g2.SetTexture("ComputerGraphicOpenGL.Textures.brickTexture.jpg"); 
            currentWorld.AddGameObject(g2);
           */

            int gameObjectsCount = 10;
            GameObject[] gameObjects = new GameObject[gameObjectsCount];
         
            for (int i = 0; i < gameObjectsCount; i++)
            {
                gameObjects[i] = new GameObject();
                gameObjects[i].Position = new Vector3(0 + (i * 105), 0 , 0);
                gameObjects[i].SetScale(100, 100, 1);
                gameObjects[i].SetTexture("ComputerGraphicOpenGL.Textures.brickTexture.jpg");
                currentWorld.AddGameObject(gameObjects[i]);

               
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            //anpassung der 3d instanzen an die neue fenstergröße

            GL.Viewport(0, 0, Size.X, Size.Y);
            projectionMatrix = Matrix4.CreateOrthographic(Size.X, Size.Y, 0.1f, 1000f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            //hier passiert das tatsächliche zeichnen von formen
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //view-projection matrix:
            Matrix4 viewProjection = viewMatrix * projectionMatrix;

            //shader programm wählen:
            GL.UseProgram(ShaderStandard.GetShaderID());

            foreach (GameObject gameobject in currentWorld.GetGameObjects())
            {
                //model matrix (zum testen): 
                Matrix4 modelMatrix = Matrix4.CreateScale(gameobject.GetScale()) 
                    * Matrix4.CreateFromQuaternion(gameobject.GetRotation()) 
                    * Matrix4.CreateTranslation(gameobject.Position); //die reihenfolge ist wichtig, erst scalieren, dann rotieren, dann bewegen

                //model-view-projektion matrix erstellen:
                Matrix4 modelViewProjection = modelMatrix * viewProjection;

               
                GL.UniformMatrix4(ShaderStandard.GetMatrixID(), false, ref modelViewProjection);

                //texture an den shader übertragen:
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, gameobject.GetTextureID());
                GL.Uniform1(ShaderStandard.GetTextureID(), 0);

                //bind triangle
                //GL.BindVertexArray(PrimitiveTriangle.GetVertexArrayObjectID());
                //GL.DrawArrays(PrimitiveType.Triangles, 0, PrimitiveTriangle.GetPointCount());

                //bind quad
                GL.BindVertexArray(PrimitiveQuad.GetVertexArrayObjectID());
                GL.DrawArrays(PrimitiveType.Triangles, 0, PrimitiveQuad.GetPointCount());

                GL.BindVertexArray(0); //0 ist entbinden

                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
           
            //0 löscht das programm
            GL.UseProgram(0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            // objekte richten sich je nach benutzerangaben neu in der welt aus

            foreach (GameObject gameobject in currentWorld.GetGameObjects())
            {
                //gibt dem update im gameobject den derzeitigen input mit
                gameobject.Update(KeyboardState, MouseState);
            }
        }
    }
}
