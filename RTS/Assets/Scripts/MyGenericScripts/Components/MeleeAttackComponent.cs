using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.Services;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class MeleeAttackComponent : ProdigyMonoBehaviour
    {
        public float AttackStrength = 10.0f;

        private List<GameObject> _objectsInAttackRange;

        protected void OnEnable()
        {
            _objectsInAttackRange = new List<GameObject>();
            KeyboardEventManager.Instance.RegisterKeyDown(KeyCode.Space, OnAttack);
        }

        protected void OnAttack(KeyCode key)
        {
            foreach (var attackableObjects in _objectsInAttackRange)
            {
                var hitComponents = attackableObjects.GetComponents(typeof(IHittable));
              
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
    }
}
