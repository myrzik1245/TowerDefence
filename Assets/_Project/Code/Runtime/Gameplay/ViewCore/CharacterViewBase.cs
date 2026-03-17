using _Project.Code.Runtime.Gameplay.Characters;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.ViewCore
{
    public abstract class CharacterViewBase : MonoBehaviour
    {
        private IDisposable _initializedSubscription;

        private void Awake()
        {
            IInitializableCharacter character = GetComponent<IInitializableCharacter>();

            _initializedSubscription = character.IsInitialized.Subscribe(OnInitialized);
        }
        
        private void OnInitialized(bool isInitialized)
        {
            if (isInitialized == false)
                return;
            
            _initializedSubscription?.Dispose();

            Initialize();
        }

        protected abstract void Initialize();
    }
}
