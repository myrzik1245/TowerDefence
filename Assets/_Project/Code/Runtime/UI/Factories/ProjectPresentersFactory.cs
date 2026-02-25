using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.UI.Factories
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer projectContainer)
        {
            _container = projectContainer;
        }
    }
}
