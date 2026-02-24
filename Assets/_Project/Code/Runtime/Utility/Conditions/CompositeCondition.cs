using System;

namespace _Project.Code.Runtime.Utility.Conditions
{
    public class CompositeCondition : ICondition
    {
        private readonly ICondition[] _conditions;
        private readonly Func<bool, bool, bool> _logicOperation;

        public CompositeCondition(params ICondition[] condtions) : this(LogicOperation.And, condtions)
        {
        }

        public CompositeCondition(Func<bool, bool, bool> logicOperation, params ICondition[] condtions)
        {
            _conditions = condtions;
            _logicOperation = logicOperation;
        }

        public bool IsCompleate()
        {
            bool result = _conditions[0].IsCompleate();

            for (int i = 1; i < _conditions.Length; i++)
                result = _logicOperation.Invoke(result, _conditions[i].IsCompleate());

            return result;
        }
    }
}