using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Framework
{
    public abstract class ProdigyMonoBehaviour : MonoBehaviour
    {
        private Transform _cachedTransform;

        protected Transform CachedTransform
        {
            get
            {
                if (_cachedTransform == null)
                    _cachedTransform = GetComponent<Transform>();

                return _cachedTransform;
            }
        }
    }
}
