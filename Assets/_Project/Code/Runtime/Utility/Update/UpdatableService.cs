using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Utility.Update
{
    public class UpdatableService : MonoBehaviour, IUpdatableService
    {
        private readonly List<IUpdate> _updates = new List<IUpdate>();
        private readonly List<ILateUpdate> _lateUpdates = new List<ILateUpdate>();
        private readonly List<IFixedUpdate> _fixedUpdates = new List<IFixedUpdate>();

        private readonly List<IUpdatable> _toAdd = new List<IUpdatable>();
        private readonly List<IUpdatable> _toRemove = new List<IUpdatable>();

        public void AddRequest(IUpdatable updatable)
        {
            if (_toAdd.Contains(updatable) == false)
                _toAdd.Add(updatable);
        }

        public void RemoveRequest(IUpdatable updatable)
        {
            if (_toRemove.Contains(updatable) == false)
                _toRemove.Add(updatable);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            foreach (IUpdatable updatableToAdd in _toAdd)
                Add(updatableToAdd);

            _toAdd.Clear();

            foreach (IUpdatable updatableToRemove in _toRemove)
                Remove(updatableToRemove);

            _toRemove.Clear();

            foreach (IUpdate update in _updates)
                update.Update(Time.deltaTime);
        }

        private void LateUpdate()
        {
            foreach (ILateUpdate updatable in _lateUpdates)
                if (updatable != null)
                    updatable.LateUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            foreach (IFixedUpdate updatable in _fixedUpdates)
                if (updatable != null)
                    updatable.FixedUpdate(Time.fixedDeltaTime);
        }

        private void Add(IUpdatable updatable)
        {
            if (updatable is IUpdate update)
                _updates.Add(update);

            if (updatable is ILateUpdate lateUpdate)
                _lateUpdates.Add(lateUpdate);

            if (updatable is IFixedUpdate fixedUpdate)
                _fixedUpdates.Add(fixedUpdate);
        }

        private void Remove(IUpdatable updatable)
        {
            if (updatable is IUpdate update)
                _updates.Remove(update);

            if (updatable is ILateUpdate lateUpdate)
                _lateUpdates.Remove(lateUpdate);

            if (updatable is IFixedUpdate fixedUpdate)
                _fixedUpdates.Remove(fixedUpdate);
        }
    }
}
