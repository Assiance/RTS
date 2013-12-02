using Assets.Scripts.MyGenericScripts.Framework;
using Assets.Scripts.MyGenericScripts.Services;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class JumpComponent : ProdigyMonoBehaviour
    {
        #region Designer Variables
        public float JumpStrength = 10.0f;
        public float FallSpeed = 5.0f;
        public JumpState CurrentJumpState = JumpState.Falling;
        #endregion

        public enum JumpState { Jumping, Falling }

        private Rigidbody2D _cachedRigidBody;

        protected void OnEnable()
        {
            //Todo: find out what is the appropiate place to put this event awake, on enable or start?
            KeyboardEventManager.instance.RegisterKeyDown(KeyCode.Space, OnJump);
            KeyboardEventManager.instance.RegisterKeyUp(KeyCode.Space, OnApplyFallSpeed);
            _cachedRigidBody = GetComponent<Rigidbody2D>();
        }

        public void Jump()
        {
            OnJump(KeyCode.None);
        }

        public void ApplyFallSpeed()
        {
            OnApplyFallSpeed(KeyCode.None);
        }

        protected void OnJump(KeyCode key)
        {
            //todo: code for AI and Player
            bool grounded = true; //todo: need a way to find if grounded since we are not using a character controller
            if (grounded)
            {
                print("Jumping");
                _cachedRigidBody.AddForce(new Vector2(0f, JumpStrength));
                CurrentJumpState = JumpState.Jumping;
            }
        }

        protected void OnApplyFallSpeed(KeyCode key)
        {
            bool grounded = false; //todo: need a way to find if grounded since we are not using a character controller
            if (CurrentJumpState == JumpState.Jumping && !grounded)
            {
                print("Fall Speed");
                _cachedRigidBody.AddForce(new Vector2(0f, -FallSpeed));
                CurrentJumpState = JumpState.Falling;
            }
        }
    }
}
