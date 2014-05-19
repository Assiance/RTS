using Assets.Scripts.MyGenericScripts.Components.AI;
using Assets.Scripts.MyGenericScripts.Components.AI.States;
using Assets.Scripts.MyGenericScripts.Components.AI.States.Model;
using Assets.Scripts.MyGenericScripts.Components.Actions;
using UnityEngine;

namespace Assets.Scripts.MyGameScripts.Gameplay.Controllers
{
    [RequireComponent(typeof(Movement))]
    public class EnemyController : FsmMachine
    {
        public GameObject FollowTarget;

        protected override void FSMOnEnable()
        {
            ConstructFSM();
        }


        private void ConstructFSM()
        {
            var followModel = new FollowStateModel()
            {
                Target = FollowTarget,
                MovementComponent = GetComponent<Movement>()
            };

            FollowState follow = new FollowState(followModel);
            follow.AddTransition(Transition.TooClose, typeof(FollowState));

            //MoveRightTestState moveRight = new MoveRightTestState();
            //moveRight.AddTransition(Transition.FarRight, typeof(MoveLeftTestState));

            //MoveLeftTestState moveLeft = new MoveLeftTestState();
            //moveLeft.AddTransition(Transition.FarLeft, typeof(MoveRightTestState));

            AddState(follow);
            //AddState(moveRight);
            //AddState(moveLeft);

            SetDefaultState(follow);
        }

        protected override void FSMUpdate()
        {
        }

        protected override void FSMFixedUpdate()
        {
            CurrentState.UpdateTransition(CachedTransform);
            CurrentState.UpdateState(CachedTransform);
        }

        public void SetTransition(Transition transition)
        {
			PerformTransition(transition);
        }
    }
}
