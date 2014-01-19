using Assets.Scripts.MyGameScripts.Gameplay.Controllers;
using Assets.Scripts.MyGenericScripts.Components;
using Assets.Scripts.MyGenericScripts.Components.AI;
using Assets.Scripts.MyGenericScripts.Components.AI.States;
using UnityEngine;

namespace Assets.Scripts.MyGameScripts.Gameplay.States
{
    public class MoveLeftTestState : FSMState
    {
        public override void Init()
        {
        }

        public override void Enter()
        {
        }

        public override void UpdateTransition(Transform npc)
        {
            if (npc.transform.position.x <= -25)
            {
                Debug.Log("Switch to Move Right state");
                npc.GetComponent<EnemyController>().SetTransition(Transition.FarLeft);
            }
        }

        public override void UpdateState(Transform npc)
        {
            npc.Translate(-1, 0, 0);
        }

        public override void Exit()
        {
        }
    }
}
