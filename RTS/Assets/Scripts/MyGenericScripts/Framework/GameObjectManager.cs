using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Framework
{
    public class GameObjectManager
    {
        private static GameObjectManager _instance;
        private static IList<GameObject> _gameObjects;
        private static IList<GameObject> _entityGameObjects;

        private GameObjectManager()
        {
            _gameObjects = new List<GameObject>();
            _entityGameObjects = new List<GameObject>();
        }

        public static GameObjectManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObjectManager();
                }

                return _instance;
            }
        }

        public void Add(GameObject theGameObject)
        {
            _gameObjects.Add(theGameObject);
        }

        public void Remove(GameObject theGameObject)
        {
            _gameObjects.Remove(theGameObject);
        }

        public static List<GameObject> GetGameObjectsThatImplement<T>()
            where T : class 
        {
            var targetObjects = (from item in _entityGameObjects
                                 where item is T
                                 select item);

            return targetObjects.ToList();
        }
    }
}
