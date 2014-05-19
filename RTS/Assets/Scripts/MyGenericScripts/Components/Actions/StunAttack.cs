using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MyGameScripts.Gameplay.Effects;
using Assets.Scripts.MyGenericScripts.Components.Actions.Interfaces;
using Assets.Scripts.MyGenericScripts.Components.General;
using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.IO;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.Actions
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(AudioSource))]
    public class StunAttack : ProdigyMonoBehaviour, IAttack
    {
        public float StunTime = 2f;
        public float EnergyCost = 10f;
        public GameObject StatsScriptObject;
        public AudioClip StunAttackClip;

        private List<GameObject> _objectsInAttackRange;
        private Stats _stats;

        protected void OnEnable()
        {
            _objectsInAttackRange = new List<GameObject>();

            _stats = StatsScriptObject == null ? GetComponent<Stats>() : CachedTransform.parent.GetComponent<Stats>();

            KeyboardEventManager.Instance.RegisterKeyDown(KeyCode.G, OnAttack);
        }

        protected void OnAttack(KeyCode key)
        {
            if (StunAttackClip != null)
                audio.PlayOneShot(StunAttackClip);

            _stats.DrainEnergy(EnergyCost);

            foreach (var attackableObjects in _objectsInAttackRange)
            {
                var hitComponents = attackableObjects.GetComponents(typeof(IHittable));

                if (hitComponents == null)
                    return;

                foreach (var hitComponent in hitComponents)
                {
                    ((IHittable)hitComponent).Hit(this);
                }

                var stunEffect = attackableObjects.AddComponent<StunEffect>();
                stunEffect.StunTime = StunTime;
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
