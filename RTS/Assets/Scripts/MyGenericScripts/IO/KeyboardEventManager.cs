using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.MyGenericScripts.IO
{
    public delegate void KeyEvent(KeyCode keyCode);

    public class KeyboardEventManager : MonoBehaviour
    {
        #region Singleton

        private static KeyboardEventManager _instance;

        public static KeyboardEventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof (KeyboardEventManager)) as KeyboardEventManager;

                    if (_instance == null)
                    {
                        _instance = new GameObject("KeyboardEventManager Temporary Instance", typeof (KeyboardEventManager)).GetComponent<KeyboardEventManager>();
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

        #endregion

        private List<KeyCode> _keys;
        private Dictionary<KeyCode, KeyEvent> _keyDownEvents;
        private Dictionary<KeyCode, KeyEvent> _keyUpEvents;

        private void Init()
        {
            _keyDownEvents = new Dictionary<KeyCode, KeyEvent>();
            _keyUpEvents = new Dictionary<KeyCode, KeyEvent>();
            _keys = new List<KeyCode>();
        }

        #region Registration

        public void RegisterKeyDown(KeyCode keyCode, KeyEvent keyEvent)
        {
            if (_keyDownEvents.ContainsKey(keyCode))
                _keyDownEvents[keyCode] += keyEvent;
            else
            {
                if (!_keys.Contains(keyCode)) _keys.Add(keyCode);
                _keyDownEvents.Add(keyCode, keyEvent);
            }
        }

        public void RegisterKeyUp(KeyCode keyCode, KeyEvent keyEvent)
        {
            if (_keyUpEvents.ContainsKey(keyCode))
                _keyUpEvents[keyCode] += keyEvent;
            else
            {
                if (!_keys.Contains(keyCode)) _keys.Add(keyCode);
                _keyUpEvents.Add(keyCode, keyEvent);
            }
        }

        public void UnregisterKeyDown(KeyCode keyCode, KeyEvent keyEvent, bool removeKey)
        {
            if (_keyDownEvents.ContainsKey(keyCode))
            {
                _keyDownEvents[keyCode] -= keyEvent;
                if (_keyDownEvents[keyCode] == null)
                    _keyDownEvents.Remove(keyCode);
            }
            if (removeKey) RemoveKey(keyCode);
        }

        public void UnregisterKeyUp(KeyCode keyCode, KeyEvent keyEvent, bool removeKey)
        {
            if (_keyUpEvents.ContainsKey(keyCode))
            {
                _keyUpEvents[keyCode] -= keyEvent;
                if (_keyUpEvents[keyCode] == null)
                    _keyUpEvents.Remove(keyCode);
            }
            if (removeKey) RemoveKey(keyCode);
        }

        public void RemoveKey(KeyCode keyCode)
        {
            if (_keyDownEvents.ContainsKey(keyCode)) _keyDownEvents.Remove(keyCode);
            if (_keyUpEvents.ContainsKey(keyCode)) _keyUpEvents.Remove(keyCode);
            if (_keys.Contains(keyCode)) _keys.Remove(keyCode);
        }

        #endregion

        #region Key detection

        protected void Update()
        {
            foreach (KeyCode key in _keys)
            {
                if (Input.GetKeyDown(key))
                    OnKeyDown(key);

                if (Input.GetKeyUp(key))
                    OnKeyUp(key);
            }
        }

        private void OnKeyDown(KeyCode keyCode)
        {
            KeyEvent keyEvent;
            if (_keyDownEvents.TryGetValue(keyCode, out keyEvent))
            {
                if (keyEvent != null)
                    keyEvent(keyCode);
            }
        }

        private void OnKeyUp(KeyCode keyCode)
        {
            KeyEvent keyEvent;
            if (_keyUpEvents.TryGetValue(keyCode, out keyEvent))
            {
                if (keyEvent != null)
                    keyEvent(keyCode);
            }
        }

        #endregion
    }
}
