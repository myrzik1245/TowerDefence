using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.Infrastructure.Registrations
{
    public class MainMenuRegistrations
    {
        public static void Register(DIContainer mainMenuContainer)
        {
            mainMenuContainer.Initialize();
        }
    }
}
