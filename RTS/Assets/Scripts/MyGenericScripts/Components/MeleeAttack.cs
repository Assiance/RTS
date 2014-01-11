using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.IO;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Stats))]
	[RequireComponent(typeof(AudioSource))]
    public class MeleeAttack : ProdigyMonoBehaviour, IAttack
    {
        public GameObject StatsScriptObject;
		public AudioClip MeleeClip;

        private List<GameObject> _objectsInAttackRange;
        private Stats _stats;

        protected void OnEnable()
        {
            _objectsInAttackRange = new List<GameObject>();

            _stats = StatsScriptObject == null ? GetComponent<Stats>() : CachedTransform.parent.GetComponent<Stats>();

            KeyboardEventManager.Instance.RegisterKeyDown(KeyCode.Space, OnAttack);
        }

        protected void OnAttack(KeyCode key)
        {
			if (MeleeClip != null)
				audio.PlayOneShot(MeleeClip);

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

        protected void OnTriggerEnter2D(Collider2D other)
        {
            _objectsInAttackRange.Add(other.gameObject);
            _objectsInAttackRange = _objectsInAttackRange.Distinct().ToList();
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            _objectsInAttackRange = _objectsInAttackRange.Distinct().ToList();
            _objectsInAttackRange.Remove(other.gameObject);
        }

        public float Damage()
        {
            return _stats.AttackStrength;
        }
    }
}
