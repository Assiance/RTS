using Assets.Scripts.MyGenericScripts.Components.Actions.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.General
{
    [RequireComponent(typeof(AudioSource))]
    public class HitSound : ProdigyMonoBehaviour, IHittable
    {
        public AudioClip Clip;

        public void Hit(IAttack hitter)
        {
            audio.PlayOneShot(Clip);
        }
    }
}
