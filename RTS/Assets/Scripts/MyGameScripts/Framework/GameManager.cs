using Assets.Scripts.MyGenericScripts.Framework.Messaging;
using UnityEngine;

namespace Assets.Scripts.MyGameScripts.Framework
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                    if (_instance == null)
                    {
                        _instance = new GameObject("GameManager Temporary Instance", typeof(GameManager)).GetComponent<GameManager>();
                    }

                    _instance.Init();
                }

                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _instance.Init();
            }
        }

        private void Init()
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
