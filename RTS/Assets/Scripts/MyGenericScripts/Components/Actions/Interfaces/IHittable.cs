using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.Actions.Interfaces
{
    public interface IHittable
    {
        void Hit(IAttack hitter);
    }
}
