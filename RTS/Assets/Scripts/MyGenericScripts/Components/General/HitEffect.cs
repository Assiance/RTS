using Assets.Scripts.MyGenericScripts.Components.Actions.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.General
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
