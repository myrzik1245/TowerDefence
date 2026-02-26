using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;

namespace _Project.Code.Runtime.Gameplay.HealthFeature
{
    public class Health : IReadOnlyHealth
    {
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<int> _currentHealth;
        private ReactiveVariable<int> _maxHealth;

        public Health(int startHealth, int maxHealth)
        {
            _currentHealth = new ReactiveVariable<int>(startHealth);
            _maxHealth = new ReactiveVariable<int>(maxHealth);
            _isDead =  new ReactiveVariable<bool>();
        }

        public IReadOnlyReactiveVariable<bool> IsDead => _isDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _currentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _maxHealth;

        public bool CanTakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(damage)} <= 0");

            return _isDead.Value == false;
        }

        public void TakeDamage(int damage)
        {
            if (CanTakeDamage(damage) == false)
                throw new InvalidOperationException();

            _currentHealth.Value = Math.Clamp(_currentHealth.Value - damage, 0, _maxHealth.Value);

            if (_currentHealth.Value <= 0)
                _isDead.Value = true;
        }

        public void Heal(int healAmount)
        {
            if (CanHeal(healAmount) == false)
                throw new InvalidOperationException();

            _currentHealth.Value = Math.Clamp(_currentHealth.Value + healAmount, 0, _maxHealth.Value);
        }

        public bool CanHeal(int healAmount)
        {
            if (healAmount <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(healAmount)} <= 0");

            if (_isDead.Value)
                return false;

            return _currentHealth.Value < _maxHealth.Value;
        }

        public void Kill()
        {
            _currentHealth.Value = 0;
            _isDead.Value = true;
        }
    }
}
