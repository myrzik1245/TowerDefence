using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.UI.Factories.Presenters
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer gameplayContainer)
        {
            _container = gameplayContainer;
        }        
    }
}
