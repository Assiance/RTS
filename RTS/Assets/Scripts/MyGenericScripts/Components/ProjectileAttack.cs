using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.IO;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Stats))]
    public class ProjectileAttack : ProdigyMonoBehaviour, IAttack
    {
        public GameObject LaunchNode;
        public GameObject LaunchObject;
        public float ProjectileSpeed = 20;

        private Stats _stats;

        protected void OnEnable()
        {
            if (LaunchNode == null)
                LaunchNode = this.gameObject;

            _stats = GetComponent<Stats>();

            KeyboardEventManager.Instance.RegisterKeyDown(KeyCode.V, OnAttack);
        }

        protected void OnAttack(KeyCode key)
        {
            var clone = Instantiate(LaunchObject, LaunchNode.transform.position, CachedTransform.rotation) as GameObject;
            var angle = CachedTransform.eulerAngles.z * Mathf.Deg2Rad;
            clone.rigidbody2D.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ProjectileSpeed;
            
            //foreach (var attackableObjects in _objectsInAttackRange)
            //{
            //    var hitComponents = attackableObjects.GetComponents(typeof(IHittable));

            //    if (hitComponents == null)
            //        return;

            //    foreach (var hitComponent in hitComponents)
            //    {
            //        ((IHittable)hitComponent).Hit(this);
            //    }
            //}
        }

        public float Damage()
        {
            return 5;
        }
    }
}
