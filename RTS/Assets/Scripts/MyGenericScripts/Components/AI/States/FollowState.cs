using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MyGameScripts.Gameplay.Controllers;
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
            if (npc.transform.position.x <= -25)
            {
                Debug.Log("Switch to Move Right state");
                npc.GetComponent<TestController>().SetTransition(Transition.FarLeft);
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
