using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.Infrastructure.Registrations
{
    public class GameplayRegistrations
    {
        public static void Register(DIContainer gameplayContainer)
        {
            gameplayContainer.Initialize();
        }
    }
}
