using System;
using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Stats))]
    public class HitDamage : ProdigyMonoBehaviour, IHittable
    {
        public Action HasDied;
        public Action HasTakenDamage;

        private IKillable _killable;
        private Stats _stats;

        protected void OnEnable()
        {
            _stats = GetComponent<Stats>();
            _killable = GetComponent(typeof (IKillable)) as IKillable;
        }

        public void Hit(IAttack hitter)
        {
            _stats.TakeDamage(hitter.Damage());

            if (HasTakenDamage != null)
                HasTakenDamage.Invoke();

            if (_stats.CurrentHealth <= 0)
            {
                _killable.Kill();

                if (HasDied != null)
                    HasDied.Invoke();
            }
        }
    }
}
