using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Utility.Conditions;
using _Project.Code.Runtime.Utility.Update;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Code.Runtime.Gameplay.AI.Brains
{
    public class BrainsContext : IDisposable, IUpdate
    {
        private readonly List<BrainData> _brainsData = new();
        private readonly List<BrainData> _toRemove = new();
        
        public void Register(ICharacter character, IBrain newBrain, ICondition releaseCondition)
        {
            BrainData newBrainData = new BrainData(character, newBrain, releaseCondition);

            BrainData oldBrainData = _brainsData.FirstOrDefault(item => item.Character == character);
            
            if (oldBrainData != null)
            {
                oldBrainData.Brain.Disable();
                oldBrainData.Brain.Dispose();
                _brainsData.Remove(oldBrainData);
            }
            
            _brainsData.Add(newBrainData);
            newBrainData.Brain.Enable();
        }

        public void Update(float deltaTime)
        {
            foreach (BrainData brainDataToRemove in _toRemove)
            {
                brainDataToRemove.Brain.Disable();
                brainDataToRemove.Brain.Dispose();
                
                _brainsData.Remove(brainDataToRemove);
            }

            _toRemove.Clear();
            
            for (int i = 0; i < _brainsData.Count; i++)
            {
                BrainData brainData = _brainsData[i];
                
                if (brainData.ReleaseCondition.IsCompleate() == false)
                    brainData.Brain.Update(deltaTime);
                else
                    _toRemove.Add(brainData);
            }
        }
        
        public void Dispose()
        {
            foreach (BrainData brain in _brainsData)
                brain.Brain.Dispose();
            
            _brainsData.Clear();
        }

        private class BrainData
        {
            public BrainData(ICharacter character, IBrain brain, ICondition releaseCondition)
            {
                Character = character;
                Brain = brain;
                ReleaseCondition = releaseCondition;
            }
            
            public ICharacter Character { get; }
            public IBrain Brain { get; }
            public ICondition ReleaseCondition { get; }
        }
    }
}
