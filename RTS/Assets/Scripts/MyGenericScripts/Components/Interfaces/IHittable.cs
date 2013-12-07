using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.Interfaces
{
    public interface IHittable
    {
        void Hit(MeleeAttackComponent hitter);
    }
}
