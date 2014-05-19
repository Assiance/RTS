using Assets.Scripts.MyGenericScripts.Components.Actions;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.AI.States.Model
{
    public class FollowStateModel
    {
        public GameObject Target { get; set; }
        public Movement MovementComponent { get; set; }
    }
}
