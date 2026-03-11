using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.UI.Core;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.WinLose
{
    public class WinLosePresenter : IPresenter
    {
        private readonly WinLoseView _view;
        private readonly WinLoseCounter _winLoseCounter;

        private List<IDisposable> _disposables = new();
        
        public WinLosePresenter(WinLoseView view, WinLoseCounter winLoseCounter)
        {
            _view = view;
            _winLoseCounter = winLoseCounter;
        }

        public void Initialize()
        {
            _view.SetWin(_winLoseCounter.WinCounter.Value);
            _view.SetLose(_winLoseCounter.LoseCounter.Value);
            
            _disposables.Add(_winLoseCounter.WinCounter.Subscribe(_view.SetWin));
            _disposables.Add(_winLoseCounter.LoseCounter.Subscribe(_view.SetLose));
        }
        
        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }
    }
}
