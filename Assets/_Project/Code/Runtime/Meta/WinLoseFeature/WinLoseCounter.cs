using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Utility.DataManagment.DataProviders;
using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Meta.WinLoseFeature
{
    public class WinLoseCounter : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private ReactiveVariable<int> _winCounter = new();
        private ReactiveVariable<int> _loseCounter = new();

        public WinLoseCounter(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }
        
        public IReadOnlyReactiveVariable<int> WinCounter => _winCounter;
        public IReadOnlyReactiveVariable<int> LoseCounter => _loseCounter;
        
        public void AddWin()
        {
            _winCounter.Value++;
        }

        public void AddLose()
        {
            _loseCounter.Value++;
        }
        
        public void ReadFrom(PlayerData data)
        {
            _winCounter.Value = data.WinCount;
            _loseCounter.Value = data.LoseCount;
        }
        
        public void WriteTo(PlayerData data)
        {
            data.WinCount = _winCounter.Value;
            data.LoseCount = _loseCounter.Value;
        }
    }
}
