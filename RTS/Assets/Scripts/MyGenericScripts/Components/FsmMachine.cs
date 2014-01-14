using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MyGenericScripts.Components.Interfaces;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public enum Transition
    {
        None = 0,
        FarRight,
        FarLeft
    }

    public class FsmMachine : ProdigyMonoBehaviour
    {
        protected FSMState DefaultState { get; set; }
        protected FSMState CurrentState { get; set; }
        protected FSMState GoalState { get; set; }

        private List<FSMState> _states;

        protected void OnEnable()
        {
            _states = new List<FSMState>();

            CurrentState = null;
            DefaultState = null;
            GoalState = null;

			Initialize();
        }

        protected virtual void Initialize() { }
        protected virtual void FSMUpdate() { }
        protected virtual void FSMFixedUpdate() { }

        protected void Update()
        {
            FSMUpdate();
        }

        protected void FixedUpdate()
        {
            //dont do anything if you have no states
            if (!_states.Any())
                return;

            //dont do anything if theres no current state
            // and no default state
            if (CurrentState == null)
                CurrentState = DefaultState;
            
            if (CurrentState == null)
                return;

            //switch if there was a transition
			if (GoalState != null && GoalState != CurrentState)
            {
                    CurrentState.Exit();
                    CurrentState = GoalState;
                    CurrentState.Enter();
            }

            FSMFixedUpdate();
        }

        public virtual void AddState(FSMState state)
        {
            // Check for Null reference before deleting
            if (state == null)
            {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
            }

            // First State inserted is also the Initial state
            //   the state the machine is in when the simulation begins
            if (_states.Count == 0)
            {
                _states.Add(state);
                DefaultState = state;
                return;
            }

            // Add the state to the List if it´s not inside it
            foreach (FSMState tempState in _states)
            {
                if (tempState.ID == state.ID)
                {
                    Debug.LogError("FSM ERROR: Trying to add a state that was already inside the list");
                    return;
                }
            }

            //If no state in the current then add the state to the list
            _states.Add(state);
        }

        /// <summary>
        /// This method delete a state from the FSM List if it exists, 
        ///   or prints an ERROR message if the state was not on the List.
        /// </summary>
        public void DeleteState(Type stateId)
        {
            // Check for NullState before deleting
            if (stateId == null)
            {
                Debug.LogError("FSM ERROR: null id is not allowed");
                return;
            }

            // Search the List and delete the state if it´s inside it
            foreach (FSMState tempState in _states)
            {
                if (tempState.ID == stateId)
                {
                    _states.Remove(tempState);
                    return;
                }
            }
            Debug.LogError("FSM ERROR: The state passed was not on the list. Impossible to delete it");
        }

        public virtual void SetDefaultState(FSMState state)
        {
            DefaultState = state;
        }

        /// <summary>
        /// This method tries to change the state the FSM is in based on
        /// the current state and the transition passed. If current state
        ///  doesn´t have a target state for the transition passed, 
        /// an ERROR message is printed.
        /// </summary>
        public void PerformTransition(Transition trans)
        {
            // Check for NullTransition before changing the current state
            if (trans == Transition.None)
            {
                Debug.LogError("FSM ERROR: Null transition is not allowed");
                return;
            }

            // Check if the currentState has the transition passed as argument
            Type targetStateID = CurrentState.GetOutputState(trans);
            if (targetStateID == null)
            {
                Debug.LogError("FSM ERROR: Current State does not have a target state for this transition");
                return;
            }

            // Update the currentStateID and currentState		
            foreach (FSMState tempState in _states)
            {
                if (tempState.ID == targetStateID)
                {
                    GoalState = tempState;
                    break;
                }
            }
        }

        public virtual void Reset()
        {
            if (CurrentState != null)
                CurrentState.Exit();
            
            CurrentState = DefaultState;

            //init all the states
            foreach (var state in _states)
            {
                state.Init();
            }

            //and now enter the m_defaultState, if any
            if (CurrentState != null)
                CurrentState.Enter();
        }
    }
}
