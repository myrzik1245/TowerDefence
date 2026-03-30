namespace _Project.Code.Runtime.Gameplay.HealthFeature
{
    public interface IDamageble
    {
        public bool CanTakeDamage(int damage);
        public void TakeDamage(int damage);
    }
}
