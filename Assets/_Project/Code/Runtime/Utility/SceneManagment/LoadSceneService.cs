using _Project.Code.Runtime.Infrastructure.EntryPoints.SceneEntryPoints;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.LoadScreen;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Code.Runtime.Utility.SceneManagment
{
    public class LoadSceneService
    {
        private ILoadScreen _loadScreen;
        private DIContainer _projectContainer;
        private DIContainer _sceneContainer;

        public LoadSceneService(ILoadScreen loadScreen, DIContainer projectContainer)
        {
            _loadScreen = loadScreen;
            _projectContainer = projectContainer;
        }

        public IEnumerator LoadAsync(string sceneName, IInputSceneArgs inputSceneArgs = null)
        {
            _loadScreen.Show();

            _sceneContainer?.Dispose();

            AsyncOperation loadEmptySceneOperation = SceneManager.LoadSceneAsync(Scenes.Empty);
            AsyncOperation loadTargetSceneOperation = SceneManager.LoadSceneAsync(sceneName);

            yield return new WaitWhile(() => loadEmptySceneOperation.isDone == false);
            yield return new WaitWhile(() => loadTargetSceneOperation.isDone == false);

            SceneEntryPoint sceneEntryPoint = UnityEngine.Object.FindObjectOfType<SceneEntryPoint>();

            if (sceneEntryPoint == null)
                throw new NullReferenceException($"Scene entry point not found");

            _sceneContainer = new DIContainer(_projectContainer);

            yield return sceneEntryPoint.Initialize(_sceneContainer, inputSceneArgs);

            _loadScreen.Hide();

            sceneEntryPoint.Run();
        }
    }
}