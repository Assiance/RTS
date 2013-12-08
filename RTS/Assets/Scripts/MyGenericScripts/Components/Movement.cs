using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class Movement : ProdigyMonoBehaviour
    {
        #region Designer Variables
        public float MovementForce = 100f;
        public float MaxSpeed = 1f;
        #endregion

        private Rigidbody2D _cachedRigidBody;
        private Vector2 _moveDelta;
        private Vector2 _moveClamp;

        protected void OnEnable()
        {
            _cachedRigidBody = GetComponent<Rigidbody2D>();
            _moveDelta = new Vector2();
            _moveClamp = new Vector2();
            //Todo: see if its possible to add get axis to the keyboard event manager
        }

        protected void FixedUpdate()
        {
            Move();
            RotateTowardsVelocity();
        }

        protected void Move()
        {
            //todo: code for AI and Player
            //todo: Remove string hardcode
            _moveDelta.x = Input.GetAxis("Horizontal");
            _moveDelta.y = Input.GetAxis("Vertical");

            MoveHorizontally();
            MoveVertically();

            _moveClamp.x = Mathf.Clamp(_cachedRigidBody.velocity.x, -MaxSpeed, MaxSpeed);
            _moveClamp.y = Mathf.Clamp(_cachedRigidBody.velocity.y, -MaxSpeed, MaxSpeed);

            _cachedRigidBody.velocity = _moveClamp;
        }

        private void MoveVertically()
        {
            var verticalVelocity = _moveDelta.y*_cachedRigidBody.velocity.y;
            if (verticalVelocity < MaxSpeed)
                _cachedRigidBody.AddForce(Vector2.up*_moveDelta.y*MovementForce);
        }

        private void MoveHorizontally()
        {
            var horizontalVelocity = _moveDelta.x*_cachedRigidBody.velocity.x;
            if (horizontalVelocity < MaxSpeed)
                _cachedRigidBody.AddForce(Vector2.right*_moveDelta.x*MovementForce);
        }

        private void RotateTowardsVelocity()
        {
            var angle = Mathf.Atan2(_cachedRigidBody.velocity.y, _cachedRigidBody.velocity.x) * Mathf.Rad2Deg;
            CachedTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
