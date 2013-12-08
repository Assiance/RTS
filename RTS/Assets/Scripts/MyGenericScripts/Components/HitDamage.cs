using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Health))]
    public class HitDamage : ProdigyMonoBehaviour, IHittable
    {
        private Health _healthComponent;

        protected void OnEnable()
        {
            _healthComponent = GetComponent<Health>();
        }

        public void Hit(IAttack hitter)
        {
            _healthComponent.TakeDamage(hitter.Damage());
        }
    }
}
