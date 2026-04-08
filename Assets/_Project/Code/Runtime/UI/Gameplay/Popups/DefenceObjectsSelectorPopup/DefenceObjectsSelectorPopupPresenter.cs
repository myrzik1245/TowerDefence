using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup.Slot;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup
{
    public class DefenceObjectsSelectorPopupPresenter : PopupPresenterBase
    {
        private readonly DefenceObjectsSelectorPopupView _view;
        private readonly DefenceObjectsSelector _selector;
        private readonly GameplayPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        
        private readonly Dictionary<DefenceObjectsSelectorSlotPresenter, DefenceObjectsSelectorSlotView> _slotsMap = new();
        
        private List<IDisposable> _disposables = new();
        
        public DefenceObjectsSelectorPopupPresenter(
            DefenceObjectsSelectorPopupView view,
            ICoroutinePerformer coroutinePerformer,
            DefenceObjectsSelector selector,
            GameplayPresentersFactory presentersFactory,
            ViewsFactory viewsFactory) : base(view, coroutinePerformer)
        {
            _view = view;
            _selector = selector;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < _selector.Datas.Count; i++)
            {
                DefenceObjectTypes type = _selector.Datas[i];
                
                DefenceObjectsSelectorSlotView slotView = _viewsFactory.Create<DefenceObjectsSelectorSlotView>(ViewIDs.DefenceObjectsSelectorSlot);
                _view.Add(slotView);
                
                DefenceObjectsSelectorSlotPresenter slotPresenter = _presentersFactory.CreateDefenceObjectsSelectorSlotPresenter(slotView, type);
                
                _slotsMap.Add(slotPresenter, slotView);
                
                slotPresenter.Initialize();
                
                int index = i;
                
                _disposables.Add(slotPresenter.ButtonClicked.Subscribe(() => OnSelected(index)));
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (KeyValuePair<DefenceObjectsSelectorSlotPresenter, DefenceObjectsSelectorSlotView> slot in _slotsMap)
            {
                slot.Key.Dispose();
                _viewsFactory.Release(slot.Value);
            }
        }

        private void OnSelected(int index)
        {
            _selector.Select(index);
        }
    }
}
