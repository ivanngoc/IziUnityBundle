using System;
using System.Collections.Generic;
using System.Reflection;
using IziHardGames.DependencyInjection.Contracts;
using IziHardGames.EventSourcing.Contracts.Infrastracture;
using IziHardGames.UnityApps.Attributes.DependencyInjection;
using IziHardGames.UnityApps.Contracts.DependencyInjection;
using IziHardGames.UnityApps.Contracts.Initializations;
using IziHardGames.UnityApps.Domain.Events;
using IziHardGames.UnityApps.Scenes.Contracts;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IziHardGames.UnityScenes
{
    public class ScenesManagerMono : MonoBehaviour, IAddServicesInstances, IInitializationComplete, IInitializationCallback, ISceneManager, IIziInject<IInMemoryEventPublisher>
    {
        public bool IsInitialized { get; private set; }
        [SerializeField] private string sceneNameToEnter = string.Empty;
        //[IziInject] private IInMemoryEventPublisher publisher;
        public IInMemoryEventPublisher Service { get; set; } = null!;
        private readonly Queue<Action> callbacks = new Queue<Action>();
        private static readonly CompletedAsyncOperation completed = new CompletedAsyncOperation();

#if UNITY_EDITOR
        public SceneAsset enterenceScene;
#endif

        private void Awake()
        {
            enabled = true;
        }

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(sceneNameToEnter))
            {
                throw new InvalidOperationException("Scene to enter is not set");
            }
        }

        private void Update()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                enabled = false;
                while (callbacks.TryDequeue(out var action))
                {
                    action();
                }
            }
        }

        public IEnumerable<(Type service, object instance)> GetServiceInstanceToAdd()
        {
            yield return (typeof(ISceneManager), this);
        }

        public AsyncOperation EnterAsync(Action<AsyncOperation, Scene>? continuation = null)
        {
            var scene = SceneManager.GetSceneByName(sceneNameToEnter);
            if (scene.isLoaded)
            {
                if (continuation != null) continuation(completed, scene);
                return completed;
            }
            else
            {
                var operation = SceneManager.LoadSceneAsync(sceneNameToEnter);
                Action<AsyncOperation> wrap = (x) =>
                {
                    var sceneInner = SceneManager.GetSceneByName(sceneNameToEnter);
                    if (continuation != null) continuation(x, sceneInner);
                    Service.Publish(new OnSceneLoadedEvent(sceneNameToEnter));
                }; ;
                operation.completed += wrap;
                return operation;
            }
        }

        public void OnInitialized()
        {

        }

        public void OnInitialized(Action action)
        {
            if (IsInitialized)
            {
                action();
                return;
            }
            callbacks.Enqueue(action);
        }
    }

    public class CompletedAsyncOperation : AsyncOperation
    {
        public CompletedAsyncOperation()
        {
            // Invoke the internal completion event so .isDone is true
            MethodInfo invokeCompletion = typeof(AsyncOperation)
                .GetMethod("InvokeCompletionEvent", BindingFlags.NonPublic | BindingFlags.Instance);
            invokeCompletion?.Invoke(this, null);
        }
    }
}
