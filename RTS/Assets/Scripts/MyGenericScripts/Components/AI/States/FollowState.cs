using Assets.Scripts.MyGenericScripts.Components.AI.States.Model;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.AI.States
{
    public class FollowState : FSMState
    {
        FollowStateModel Model { get; set; }

        public FollowState(FollowStateModel model)
        {
            Model = model;
        }

        public override void Init()
        {
            
        }

        public override void Enter()
        {
        }

        public override void UpdateTransition(Transform npc)
        {
            //if (npc.position.magnitude <= Target.transform.position.magnitude)
            //{
            //    Debug.Log("Switch to Move Right state");
            //    npc.GetComponent<TestController>().SetTransition(Transition.TooClose);
            //}
        }

        public override void UpdateState(Transform npc)
        {
            if (Model.Target.transform.position.x > npc.position.x)
                Model.MovementComponent.SetHorizontalMoveDelta(1);

            if (Model.Target.transform.position.x < npc.position.x)
                Model.MovementComponent.SetHorizontalMoveDelta(-1);

            if (Model.Target.transform.position.y > npc.position.y)
                Model.MovementComponent.SetVerticalMoveDelta(1);

            if (Model.Target.transform.position.y < npc.position.y)
                Model.MovementComponent.SetVerticalMoveDelta(-1);
        }

        public override void Exit()
        {
        }
    }
}
