using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Gameplay.StatsFeature
{
    public class Stats
    {
        private readonly Dictionary<StatTypes, Stat> _stats =  new();

        public Stats(params Stat[] stats)
        {
            foreach (Stat stat in stats)
                _stats.Add(stat.Type, stat);
        }

        public void Change(StatTypes type, Func<int, int> changer)
        {
            _stats[type].Change(changer);
        }
        
        public Stat Get(StatTypes type)
        {
            return _stats[type];
        }

        public void Add(StatTypes type, Stat stat)
        {
            _stats.Add(type, stat);
        }
    }
}
