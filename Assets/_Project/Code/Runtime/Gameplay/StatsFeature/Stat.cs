using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;

namespace _Project.Code.Runtime.Gameplay.StatsFeature
{
    public class Stat
    {
        public Stat(ReactiveVariable<int> baseStat, StatTypes type)
        {
            BaseStat = baseStat;
            ModifiedStat = new ReactiveVariable<int>(BaseStat.Value);
            Type = type;
        }

        public ReactiveVariable<int> BaseStat { get; }
        public ReactiveVariable<int> ModifiedStat { get; }
        public StatTypes Type { get; }
        
        public void Reset()
        {
            ModifiedStat.Value = BaseStat.Value;
        }

        public void Change(Func<int, int> modifier)
        {
            ModifiedStat.Value = modifier(BaseStat.Value);
        }
    }
}
