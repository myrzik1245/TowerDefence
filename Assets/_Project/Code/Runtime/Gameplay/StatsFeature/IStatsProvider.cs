namespace _Project.Code.Runtime.Gameplay.StatsFeature
{
    public interface IStatsProvider
    {
        Stat Get(StatTypes statType);
    }
}
