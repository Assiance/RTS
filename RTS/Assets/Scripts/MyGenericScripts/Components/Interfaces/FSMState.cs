using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.Interfaces
{
    public abstract class FSMState
    {
        protected Dictionary<Transition, Type> map = new Dictionary<Transition, Type>();
		public Type ID 
		{ 
			get { return this.GetType(); } 
		}

        public abstract void Init();
        public abstract void Enter();
        public abstract void UpdateTransition(Transform npc);
        public abstract void UpdateState(Transform npc);
        public abstract void Exit();

        public void AddTransition(Transition transition, Type id)
        {
            // Check if anyone of the args is invallid
            if (transition == Transition.None || id == null)
            {
                Debug.LogWarning("FSMState : Null transition not allowed");
                return;
            }

            //Since this is a Deterministc FSM,
            //Check if the current transition was already inside the map
            if (map.ContainsKey(transition))
            {
                Debug.LogWarning("FSMState ERROR: transition is already inside the map");
                return;
            }

            map.Add(transition, id);
            Debug.Log("Added : " + transition + " with ID : " + id);
        }

        /// <summary>
        /// This method deletes a pair transition-state from this state´s map.
        /// If the transition was not inside the state´s map, an ERROR message is printed.
        /// </summary>
        public void DeleteTransition(Transition trans)
        {
            // Check for NullTransition
            if (trans == Transition.None)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return;
            }

            // Check if the pair is inside the map before deleting
            if (map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }
            Debug.LogError("FSMState ERROR: Transition passed was not on this State´s List");
        }


        /// <summary>
        /// This method returns the new state the FSM should be if
        ///    this state receives a transition  
        /// </summary>
        public Type GetOutputState(Transition trans)
        {
            // Check for NullTransition
            if (trans == Transition.None)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return null;
            }

            // Check if the map has this transition
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }

            Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the State was not on the list");
            return null;
        }
    }
}

