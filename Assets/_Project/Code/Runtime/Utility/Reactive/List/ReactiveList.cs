using _Project.Code.Runtime.Utility.Reactive.Event;
using System.Collections;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.Reactive.List
{
    public class ReactiveList<T> : IReadOnlyReactiveList<T>
    {
        private readonly List<T> _list = new();
        private readonly ReactiveEvent<T> _added = new();
        private readonly ReactiveEvent<T> _removed = new();
        
        public ReactiveList()
        {
        }

        public ReactiveList(List<T> list)
        {
            _list = list;
        }
        
        public IReadOnlyReactiveEvent<T> Added => _added;
        public IReadOnlyReactiveEvent<T> Removed => _removed;
        public IReadOnlyList<T> List => _list;

        public void Add(T item)
        {
            _list.Add(item);
            _added.Invoke(item);
        }

        public void Remove(T item)
        {
            _list.Remove(item);
            _removed.Invoke(item);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _list.Clear();
        }
    }

}
