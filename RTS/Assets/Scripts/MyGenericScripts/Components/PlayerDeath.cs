using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class PlayerDeath : ProdigyMonoBehaviour, IKillable
    {
        public void Kill()
        {
            renderer.enabled = false;
        }
    }
}
