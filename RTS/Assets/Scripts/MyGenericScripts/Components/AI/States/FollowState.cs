using Assets.Scripts.MyGenericScripts.Components.AI.States.Model;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.AI.States
{
    public class FollowState : FSMState
    {
        GameObject Target;

        public FollowState(FollowStateModel model)
        {
            Target = model.Target;
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
            var speed = new Vector2();

            if (Target.transform.position.x > npc.position.x)
                speed.x = 1;

            if (Target.transform.position.x < npc.position.x)
                speed.x = -1;

            if (Target.transform.position.y > npc.position.y)
                speed.y = 1;

            if (Target.transform.position.y < npc.position.y)
                speed.y = -1;

            npc.rigidbody2D.velocity = speed;
        }

        public override void Exit()
        {
        }
    }
}
