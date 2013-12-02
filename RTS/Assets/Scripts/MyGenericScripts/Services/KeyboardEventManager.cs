using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.MyGenericScripts.Services
{
    public delegate void KeyEvent(KeyCode K);

    public class KeyboardEventManager : MonoBehaviour
    {
        #region Singleton

        private static KeyboardEventManager m_Instance;

        public static KeyboardEventManager instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType(typeof (KeyboardEventManager)) as KeyboardEventManager;

                    if (m_Instance == null)
                    {
                        m_Instance = new GameObject("KeyboardEventManager Temporary Instance", typeof (KeyboardEventManager)).GetComponent<KeyboardEventManager>();
                    }

                    m_Instance.Init();
                }
                return m_Instance;
            }
        }

        private void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                m_Instance.Init();
            }
        }

        #endregion

        private List<KeyCode> keys;
        private Dictionary<KeyCode, KeyEvent> keyDownEvents, keyUpEvents;

        private void Init()
        {
            keyDownEvents = new Dictionary<KeyCode, KeyEvent>();
            keyUpEvents = new Dictionary<KeyCode, KeyEvent>();
            keys = new List<KeyCode>();
        }

        #region Registration

        public void RegisterKeyDown(KeyCode K, KeyEvent kEvent)
        {
            if (keyDownEvents.ContainsKey(K))
                keyDownEvents[K] += kEvent;
            else
            {
                if (!keys.Contains(K)) keys.Add(K);
                keyDownEvents.Add(K, kEvent);
            }
        }

        public void RegisterKeyUp(KeyCode K, KeyEvent kEvent)
        {
            if (keyUpEvents.ContainsKey(K))
                keyUpEvents[K] += kEvent;
            else
            {
                if (!keys.Contains(K)) keys.Add(K);
                keyUpEvents.Add(K, kEvent);
            }
        }

        public void UnregisterKeyDown(KeyCode K, KeyEvent kEvent, bool removeKey)
        {
            if (keyDownEvents.ContainsKey(K))
            {
                keyDownEvents[K] -= kEvent;
                if (keyDownEvents[K] == null)
                    keyDownEvents.Remove(K);
            }
            if (removeKey) RemoveKey(K);
        }

        public void UnregisterKeyUp(KeyCode K, KeyEvent kEvent, bool removeKey)
        {
            if (keyUpEvents.ContainsKey(K))
            {
                keyUpEvents[K] -= kEvent;
                if (keyUpEvents[K] == null)
                    keyUpEvents.Remove(K);
            }
            if (removeKey) RemoveKey(K);
        }

        public void RemoveKey(KeyCode K)
        {
            if (keyDownEvents.ContainsKey(K)) keyDownEvents.Remove(K);
            if (keyUpEvents.ContainsKey(K)) keyUpEvents.Remove(K);
            if (keys.Contains(K)) keys.Remove(K);
        }

        #endregion

        #region Key detection

        private void Update()
        {
            foreach (KeyCode key in keys)
            {
                if (Input.GetKeyDown(key))
                    OnKeyDown(key);

                if (Input.GetKeyUp(key))
                    OnKeyUp(key);
            }
        }

        private void OnKeyDown(KeyCode K)
        {
            KeyEvent E = null;
            if (keyDownEvents.TryGetValue(K, out E))
                if (E != null)
                    E(K);
        }

        private void OnKeyUp(KeyCode K)
        {
            KeyEvent E = null;
            if (keyUpEvents.TryGetValue(K, out E))
                if (E != null)
                    E(K);
        }

        #endregion
    }
}
