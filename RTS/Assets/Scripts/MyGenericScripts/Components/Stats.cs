using System.Collections;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    public class Stats : MainGameObjectBehaviour
    {
        public float MaxHealth = 100f;
        public float MaxEnergy = 100f;
        public float AttackStrength = 10f;
        public float ProjectileStrength = 10f;
        public float MovementForce = 100f;
        public float MaxSpeed = 1f;
        public float EnergyRegenerationRate = 3f;
        public float EnergyRegenerationAmount = 10f;
        public float CurrentHealth { get; private set; }
        public float CurrentEnergy { get; private set; }

        private HealthBar _healthBar;
        private EnergyBar _energyBar;

        protected override void OnEnable()
        {
            base.OnEnable();

            _healthBar = GetComponent<HealthBar>();
            _energyBar = GetComponent<EnergyBar>();
            CurrentHealth = MaxHealth;
            CurrentEnergy = MaxEnergy;

            if (_energyBar != null)
                InvokeRepeating("RegenerateEnergy", 0f, EnergyRegenerationRate);
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (_healthBar != null)
                _healthBar.UpdateHealthBar();
        }

        public void DrainEnergy(float energyDrained)
        {
            CurrentEnergy -= energyDrained;

            if (_energyBar != null)
                _energyBar.UpdateEnergyBar();
        }

        public void RegenerateEnergy()
        {
            CurrentEnergy += EnergyRegenerationAmount;

            if (CurrentEnergy >= MaxEnergy)
                CurrentEnergy = MaxEnergy;

            if (_energyBar != null)
                _energyBar.UpdateEnergyBar();
        }
    }
}
