using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Health))]
    public class HitDamage : ProdigyMonoBehaviour, IHittable
    {
        //HasTakenDamage delegate action
        //HasDied delegate action
        private Health _healthComponent;
        private IKillable _killable;

        protected void OnEnable()
        {
            _healthComponent = GetComponent<Health>();
            _killable = GetComponent(typeof (IKillable)) as IKillable;
        }

        public void Hit(IAttack hitter)
        {
            _healthComponent.TakeDamage(hitter.Damage());

            if (_healthComponent.CurrentHealth <= 0)
            {
                _killable.Kill();
            }
        }
    }
}
