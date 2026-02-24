using _Project.Code.Runtime.Infrastructure.Registrations;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using System.Collections;

namespace _Project.Code.Runtime.Infrastructure.EntryPoints.SceneEntryPoints
{
    public class MainMenuEntryPoint : SceneEntryPoint
    {
        public override IEnumerator Initialize(DIContainer container, IInputSceneArgs inputSceneArgs)
        {
            MainMenuRegistrations.Register(container);

            yield break;
        }

        public override void Run()
        {
        }
    }
}
