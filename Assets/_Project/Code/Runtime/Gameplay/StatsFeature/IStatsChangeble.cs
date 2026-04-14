using System;

namespace _Project.Code.Runtime.Gameplay.StatsFeature
{
    public interface IStatsChangeable
    {
        public void ChangeStat(StatTypes type, Func<int, int> changer);
    }
}
