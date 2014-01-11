using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Stats))]
    public class Movement : ProdigyMonoBehaviour
    {
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

			//Will keep object from moving after a collision
			_cachedRigidBody.angularVelocity = 0f;
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
