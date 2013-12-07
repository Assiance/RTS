using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(HealthComponent))]
    public class HitDamageCompoent : ProdigyMonoBehaviour, IHittable
    {
        private HealthComponent _healthComponent;

        protected void OnEnable()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        public void Hit(MeleeAttackComponent hitter)
        {
            _healthComponent.TakeDamage(hitter.AttackStrength);
        }
    }
}
