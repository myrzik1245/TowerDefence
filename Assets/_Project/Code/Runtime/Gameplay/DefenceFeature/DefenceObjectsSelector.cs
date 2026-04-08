using _Project.Code.Runtime.Utility.Selector;

namespace _Project.Code.Runtime.Gameplay.DefenceFeature
{
    public class DefenceObjectsSelector : SelectorService<DefenceObjectTypes>
    {
        public DefenceObjectsSelector(params DefenceObjectTypes[] datas) : base(datas)
        {
        }
    }
}
