using System.Collections.Generic;

namespace _Project.Code.Runtime.Gameplay.AI
{
    public class Blackboard
    {
        private readonly Dictionary<string, object> _data = new();

        public void WriteData(string key, object value)
        {
            _data[key] = value;
        }

        public bool TryGetData<TData>(string key, out TData data)
        {
            data = default;
            
            if (_data.TryGetValue(key, out object value))
            {
                data = (TData)value;
                
                if (data != null)
                    return true;
            }

            return false;
        }
    }
}
