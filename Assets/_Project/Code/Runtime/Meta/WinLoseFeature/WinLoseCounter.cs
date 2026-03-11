using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Meta.WinLoseFeature
{
    public class WinLoseCounter
    {
        private ReactiveVariable<int> _winCounter = new();
        private ReactiveVariable<int> _loseCounter = new();
        
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
    }
}
