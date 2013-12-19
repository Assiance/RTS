using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.IO;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class ProjectileLaunch : ProdigyMonoBehaviour
    {
        public GameObject LaunchNode;
        public GameObject ProjectileObject;
        public AudioClip LaunchClip;
        public float ProjectileSpeed = 20;
        public bool CanFire = true;
        public float FireRateInSeconds = 1f;

        private float _timeElapsedSinceFired;

        protected void OnEnable()
        {
            if (LaunchNode == null)
                LaunchNode = this.gameObject;

            KeyboardEventManager.Instance.RegisterKeyDown(KeyCode.V, OnLanuch);
        }

        protected void Update()
        {
            if (CanFire == false)
            {
                if (_timeElapsedSinceFired >= FireRateInSeconds)
                {
                    CanFire = true;
                }
                else
                {
                    _timeElapsedSinceFired += Time.fixedDeltaTime;
                }
            }
        }

        protected void OnLanuch(KeyCode key)
        {
            if (CanFire)
            {
                var clone = Instantiate(ProjectileObject, LaunchNode.transform.position, CachedTransform.rotation) as GameObject;

                if (clone == null)
                    throw new MissingReferenceException();

                var angle = CachedTransform.eulerAngles.z * Mathf.Deg2Rad;
                clone.rigidbody2D.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ProjectileSpeed;

                if (LaunchClip != null)
                    audio.PlayOneShot(LaunchClip);

                _timeElapsedSinceFired = 0f;
                CanFire = false;
            }
        }
    }
}
