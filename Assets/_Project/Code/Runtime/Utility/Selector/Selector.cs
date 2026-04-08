using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.Selector
{
    public class SelectorService<TSelectData>
    {
        private readonly TSelectData[] _datas;

        private TSelectData _data;
        
        public SelectorService(params TSelectData[] datas)
        {
            _datas = datas;
            _data = _datas[0];
        }

        public IReadOnlyList<TSelectData> Datas => _datas;
        
        public TSelectData Get()
        {
            return _data;
        }

        public void Select(int index)
        {
            _data = _datas[index];
        }
    }
}