using Assets.Scripts.MyGenericScripts.Components.General;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.Actions
{
    [RequireComponent(typeof(Stats))]
    public class Movement : ProdigyMonoBehaviour
    {
        public bool IsAiControlled = true;
        public bool ShouldRotate = true;
     
        private Stats _stats;
        private Rigidbody2D _cachedRigidBody;
        private Vector2 _moveDelta;

        protected void OnEnable()
        {
            _stats = GetComponent<Stats>();
            _cachedRigidBody = GetComponent<Rigidbody2D>();
            _moveDelta = new Vector2();
            //Todo: see if its possible to add get axis to the keyboard event manager
        }

        protected void FixedUpdate()
        {
            Move();

            if (ShouldRotate)
                RotateTowardsVelocity();
        }

        protected void Move()
        {
            //todo: code for AI and Player
            //todo: Remove string hardcode
            if (IsAiControlled == false)
            {
                SetHorizontalMoveDelta(Input.GetAxis("Horizontal"));
                SetVerticalMoveDelta(Input.GetAxis("Vertical"));   
            }

            MoveHorizontally();
            MoveVertically();

			//Will keep object from moving after a collision
			_cachedRigidBody.angularVelocity = 0f;
        }

        public void SetHorizontalMoveDelta(float horizontalInput)
        {
            _moveDelta.x = horizontalInput;
        }

        public void SetVerticalMoveDelta(float verticalInput)
        {
            _moveDelta.y = verticalInput;
        }

        private void MoveHorizontally()
        {
            _cachedRigidBody.velocity = new Vector2(_moveDelta.x * _stats.MaxSpeed, _cachedRigidBody.velocity.y);
        }

        private void MoveVertically()
        {
            _cachedRigidBody.velocity = new Vector2(_cachedRigidBody.velocity.x, _moveDelta.y * _stats.MaxSpeed);
        }

        private void RotateTowardsVelocity()
        {
            var angle = Mathf.Atan2(_cachedRigidBody.velocity.y, _cachedRigidBody.velocity.x) * Mathf.Rad2Deg;
            CachedTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
