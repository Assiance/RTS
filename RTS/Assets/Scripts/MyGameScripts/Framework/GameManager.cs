using Assets.Scripts.MyGenericScripts.Framework.Messaging;
using UnityEngine;

namespace Assets.Scripts.MyGameScripts.Framework
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        private GameManager()
        {
        }

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    //Todo: This wont work. because gamemanager is monbehaviour
                    _instance = new GameManager();
                }

                return _instance;
            }
        }

        protected void Awake()
        {
        }

        protected void Start()
        {
        }

        protected void Update()
        {
            MessagePump.Instance.Update(Time.fixedDeltaTime);
        }
    }
}
