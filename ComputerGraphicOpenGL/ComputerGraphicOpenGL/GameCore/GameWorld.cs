using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphicOpenGL.GameCore
{
    class GameWorld
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<LightObject> lightObjects = new List<LightObject>();

        //gameobjects
        public void AddGameObject(GameObject g)
        {
            gameObjects.Add(g);
        }

        public bool RemoveGameObject(GameObject g)
        {
            return gameObjects.Remove(g);
        }

        public List<GameObject> GetGameObjects()
        {
            return gameObjects;
        }



        //lightobjects
        public void AddLightObject(LightObject l)
        {
            lightObjects.Add(l);
        }

        public bool RemoveLightObject(LightObject l)
        {
            return lightObjects.Remove(l);
        }


    }
}
