using Assets.Scripts.MyGameScripts.Gameplay.States;
using Assets.Scripts.MyGenericScripts.Components;

namespace Assets.Scripts.MyGameScripts.Gameplay.Controllers
{
    public class TestController : FsmMachine 
    {
        protected override void Initialize()
        {
            ConstructFSM();
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

        private void ConstructFSM()
        {
            MoveRightTestState moveRight = new MoveRightTestState();
            moveRight.AddTransition(Transition.FarRight, typeof(MoveLeftTestState));

            MoveLeftTestState moveLeft = new MoveLeftTestState();
            moveLeft.AddTransition(Transition.FarLeft, typeof(MoveRightTestState));

            AddState(moveRight);
            AddState(moveLeft);

            SetDefaultState(moveRight);
        }
    }
}
