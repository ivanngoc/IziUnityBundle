//using System;
//using System.Threading.Tasks;
//using IziHardGames.Apps.Abstractions.Lib;
//using UnityEngine;
//using static IziHardGames.Apps.Abstractions.ForUnity.Presets.ConstantsForUnityEditorMenus;

//namespace IziHardGames.AppConstructor
//{
//    /// <summary>
//    /// Объект для синхронизации сцен и межсценового взаимодействия и других точек синхронизаций
//    /// </summary>

//    [CreateAssetMenu(fileName = nameof(ScriptableApp), menuName = NAME_ROOT_MENU_NAME + "/" + NAME_MENU_CATEGORY_SYNC_POINTS + "/" + nameof(ScriptableApp))]
//    public class ScriptableApp : IziScriptableObject, IDisposable
//    {
//        public IIziApp App { get; private set; }
//        public IIziServiceProvider ServiceProvider { get => serviceProvider ?? throw new NullReferenceException(); }
//        private IIziServiceProvider serviceProvider;
//        private TaskCompletionSource<bool> tcs = null;

//        private void OnEnable()
//        {
//            Debug.Log("Enabled", this);
//            tcs = new TaskCompletionSource<bool>();
//        }
//        private void OnDisable()
//        {
//            Debug.Log("Disabled", this);
//            tcs = new TaskCompletionSource<bool>();
//        }
//        private void OnValidate()
//        {
//            Debug.Log("Validated", this);
//            tcs = new TaskCompletionSource<bool>();
//        }


//        public void Initilize(IIziApp app)
//        {
//            App = app;
//            serviceProvider = (app.ServiceProvider as IIziServiceProvider) ?? throw new NullReferenceException();
//            tcs.SetResult(true);
//        }
//        public Task AwaitInitilization()
//        {
//            return tcs.Task;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="syncPointName"></param>
//        /// <returns></returns>
//        /// <exception cref="NotImplementedException"></exception>
//        public Task AwaitSyncPoint(string syncPointName)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// Отправить сигнал о завершении ожидания точки синхронизации
//        /// </summary>
//        public void SetSyncPoint()
//        {
//            throw new NotImplementedException();
//        }

//        public void CompleteCallback(int buildIndex)
//        {
//            //Debug.LogException(new NotImplementedException());
//        }

//        public void Dispose()
//        {
//            if (tcs.Task.Status != TaskStatus.RanToCompletion)
//            {
//                tcs.SetCanceled();
//            }
//            tcs = new TaskCompletionSource<bool>();
//        }
//    }
//}