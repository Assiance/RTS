using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Stats))]
    public class ContactAttack : ProdigyMonoBehaviour, IAttack
    {
        public ParticleSystem TrailParticle;

        private Stats _stats;
        
        protected void OnEnable()
        {
            _stats = GetComponent<Stats>();
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            this.renderer.enabled = false;
            Destroy(this.gameObject);
        }

        public float Damage()
        {
            return _stats.AttackStrength;
        }
    }
}
