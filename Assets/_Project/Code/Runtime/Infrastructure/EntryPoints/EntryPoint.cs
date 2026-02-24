using _Project.Code.Runtime.Infrastructure.Registrations;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.LoadScreen;
using _Project.Code.Runtime.Utility.SceneManagment;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Infrastructure.EntryPoints
{
    public class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            DIContainer projectContainer = new DIContainer();
            ProjectRegistrations.Register(projectContainer);

            ICoroutinePerformer coroutinePerformer = projectContainer.Resolve<ICoroutinePerformer>();

            coroutinePerformer.StartPerform(Initialize(projectContainer));
        }

        private IEnumerator Initialize(DIContainer projectContainer)
        {
            ILoadScreen loadScreen = projectContainer.Resolve<ILoadScreen>();

            loadScreen.Show();

            ConfigsProvider configsProvider = projectContainer.Resolve<ConfigsProvider>();
            LoadSceneService loadSceneService = projectContainer.Resolve<LoadSceneService>();

            yield return configsProvider.LoadAsync();
            yield return loadSceneService.LoadAsync(Scenes.MainMenu);

            loadScreen.Hide();
        }
    }
}
