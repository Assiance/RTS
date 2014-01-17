using Assets.Scripts.MyGenericScripts.Components.Actions.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;

namespace Assets.Scripts.MyGenericScripts.Components.Actions
{
    public class PlayerDeath : ProdigyMonoBehaviour, IKillable
    {
        public void Kill()
        {
            renderer.enabled = false;
        }
    }
}
