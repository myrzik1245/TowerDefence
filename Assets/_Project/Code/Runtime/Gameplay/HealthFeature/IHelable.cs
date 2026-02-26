namespace _Project.Code.Runtime.Gameplay.HealthFeature
{
    public interface IHelable
    {
        public bool CanHeal(int healAmount);
        public void Heal(int healAmount);
    }
}
