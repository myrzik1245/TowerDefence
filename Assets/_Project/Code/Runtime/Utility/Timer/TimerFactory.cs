using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.Utility.Timer
{
    public class TimerFactory
    {
        private readonly ICoroutinePerformer _coroutinePerformer;
        
        public TimerFactory(DIContainer container)
        {
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
        }

        public TimerService Create(float time)
        {
            return new TimerService(_coroutinePerformer, time);
        }
    }
}