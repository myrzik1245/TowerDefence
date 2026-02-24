using System;

namespace _Project.Code.Runtime.Utility.Conditions
{
    public class FuncCondition : ICondition
    {
        private readonly Func<bool> _condition;

        public FuncCondition(Func<bool> condition)
        {
            _condition = condition;
        }

        public bool IsCompleate()
        {
            return _condition.Invoke();
        }
    }
}