using Assets.Scripts.MyGenericScripts.Framework;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class Stats : MainGameObjectBehaviour
    {
        public float MaxHealth = 100f;
        public float AttackStrength = 10f;
        public float MovementForce = 100f;
        public float MaxSpeed = 1f;
        public float CurrentHealth { get; private set; }
        
        private HealthBar _healthBar;

        protected override void OnEnable()
        {
            base.OnEnable();

            _healthBar = GetComponent<HealthBar>();
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (_healthBar != null)
                _healthBar.UpdateHealthBar();
        }
    }
}
