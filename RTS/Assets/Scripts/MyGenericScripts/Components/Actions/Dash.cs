using System.Collections;
using Assets.Scripts.MyGenericScripts.Components.General;
using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.IO;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.Actions
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(AudioSource))]
    public class Dash : ProdigyMonoBehaviour
    {
        public float DashSpeed = 5f;
        public float DashDurationTime = 1f;
        public ParticleSystem DashParticle;
        public float EnergyCost = 10f;
        public AudioClip DashClip;
        public bool CanDash = true;
        public float DashRate = 5f;

        private Stats _stats;
        private Movement _movement;
        private Rigidbody2D _cachedRigidBody;

        protected void OnEnable()
        {
            _stats = GetComponent<Stats>();
            _movement = GetComponent<Movement>();
            _cachedRigidBody = GetComponent<Rigidbody2D>();

            KeyboardEventManager.Instance.RegisterKeyDown(KeyCode.LeftShift, OnDash);
        }

        protected void OnDash(KeyCode key)
        {
            bool HasEnergy = _stats.CurrentEnergy >= EnergyCost;

            if (CanDash && HasEnergy)
            {
                StartCoroutine(InitiateDash());
                StartCoroutine(DashWait());
            }
        }

        IEnumerator InitiateDash()
        {
            var originalMaxSpeed = _stats.MaxSpeed;
            _stats.MaxSpeed += DashSpeed;
            _stats.DrainEnergy(EnergyCost);

            if (DashParticle != null)
                DashParticle.Play();

            if (DashClip != null)
                audio.PlayOneShot(DashClip);

            yield return new WaitForSeconds(DashDurationTime);
            _stats.MaxSpeed = originalMaxSpeed;

            if (DashClip != null)
                audio.Stop();
        }

        IEnumerator DashWait()
        {
            CanDash = false;

            yield return new WaitForSeconds(DashRate);
            CanDash = true;
        }
    }
}

