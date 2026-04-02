namespace _Project.Code.Runtime.Utility.Selector
{
    public class SelectorService<TSelectData>
    {
        private readonly TSelectData[] _datas;

        private TSelectData _data;
        
        public SelectorService(params TSelectData[] datas)
        {
            _datas = datas;
            _data = datas[0];
        }

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