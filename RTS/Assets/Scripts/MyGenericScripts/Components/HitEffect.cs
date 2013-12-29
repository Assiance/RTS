using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class HitEffect : ProdigyMonoBehaviour, IHittable
    {
        public ParticleSystem Particle;

        public void Hit(IAttack hitter)
        {
            if (Particle != null)
                Particle.Play();   
        }
    }
}
