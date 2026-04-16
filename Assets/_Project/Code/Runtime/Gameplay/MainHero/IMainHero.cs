using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.StatsFeature;

namespace _Project.Code.Runtime.Gameplay.MainHero
{
    public interface IMainHero : ICharacter, IAttack, IStatsChangeable
    {
    }
}
