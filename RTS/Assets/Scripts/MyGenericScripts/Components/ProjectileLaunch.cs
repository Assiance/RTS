using System.Collections;
using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.IO;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Stats))]
	[RequireComponent(typeof(AudioSource))]
    public class ProjectileLaunch : ProdigyMonoBehaviour
    {
        public GameObject LaunchNode;
        public GameObject ProjectileObject;
        public AudioClip LaunchClip;
        public float EnergyCost = 10f;
        public float ProjectileSpeed = 20;
        public bool CanFire = true;
        public float FireRateInSeconds = 1f;

        private Stats _stats;

        protected void OnEnable()
        {
            _stats = GetComponent<Stats>();

            if (LaunchNode == null)
                LaunchNode = this.gameObject;

            KeyboardEventManager.Instance.RegisterKeyDown(KeyCode.V, OnLaunch);
        }

        protected void OnLaunch(KeyCode key)
        {
            if (CanFire)
            {
                var projectileClone = Instantiate(ProjectileObject, LaunchNode.transform.position, CachedTransform.rotation) as GameObject;

                if (projectileClone == null)
                    throw new MissingReferenceException();

                SetProjectileAttackStrength(projectileClone);
                SetProjectileDirectionAndSpeed(projectileClone);

                if (LaunchClip != null)
                    audio.PlayOneShot(LaunchClip);

                _stats.DrainEnergy(EnergyCost);
                StartCoroutine(ProjectileWait());
            }
        }

        private void SetProjectileDirectionAndSpeed(GameObject projectileClone)
        {
            var angle = CachedTransform.eulerAngles.z*Mathf.Deg2Rad;
            projectileClone.rigidbody2D.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*ProjectileSpeed;
        }

        private void SetProjectileAttackStrength(GameObject clone)
        {
            var contactAttack = clone.GetComponent<ContactAttack>();

            if (contactAttack != null)
                contactAttack.AttackStrength = _stats.ProjectileStrength;
        }

        IEnumerator ProjectileWait()
        {
            CanFire = false;

            yield return new WaitForSeconds(FireRateInSeconds);
            CanFire = true;
        }
    }
}
