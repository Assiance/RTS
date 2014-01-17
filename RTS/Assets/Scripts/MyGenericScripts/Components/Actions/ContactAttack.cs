using System.Collections.Generic;
using Assets.Scripts.MyGenericScripts.Framework;
using System.Linq;
using UnityEngine;
using Assets.Scripts.MyGenericScripts.Components.Actions.Interfaces;

namespace Assets.Scripts.MyGenericScripts.Components.Actions
{
    public class ContactAttack : ProdigyMonoBehaviour, IAttack
    {
        public ParticleSystem TrailParticle;
        public ParticleSystem ExplosionParticle;
        public bool FriendlyFire = true;
		public float ExplosionRadius = 2f;
        public float AttackStrength = 10f;
        public AudioClip ExplosionClip;

        private List<Collider2D> _objectsInAttackRange;
        
        protected void OnEnable()
        {
            _objectsInAttackRange = new List<Collider2D>();
        }

        protected void Attack()
        {
            foreach (var attackableObjects in _objectsInAttackRange)
            {
                var hitComponents = attackableObjects.GetComponents(typeof(IHittable));

                if (hitComponents == null)
                    return;

                foreach (var hitComponent in hitComponents)
                {
                    ((IHittable)hitComponent).Hit(this);
                }
            }
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            _objectsInAttackRange = Physics2D.OverlapCircleAll(CachedTransform.position, ExplosionRadius).ToList();

            if (FriendlyFire)
                _objectsInAttackRange.Remove(this.collider2D);

            Attack();

			if (ExplosionParticle != null)
				ExplosionParticle.Play();
			
			if (ExplosionClip != null)
                audio.PlayOneShot(ExplosionClip);

            TrailParticle.Stop();
            this.collider2D.enabled = false;
            this.renderer.enabled = false;
			this.gameObject.rigidbody2D.velocity = Vector2.zero;
			this.gameObject.rigidbody2D.fixedAngle = true;

			Destroy(ExplosionParticle, 2f);
			Destroy(this.gameObject, 2f);
        }

        public float Damage()
        {
            return AttackStrength;
        }
    }
}
