//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IziHardGames.Apps.Abstractions.Lib;
//using UnityEngine;

//namespace IziHardGames.AppConstructor
//{
//    public class IziBootstrap : MonoBehaviour
//    {
//        [SerializeField] public IziAppScheme? iziAppScheme;
//        [SerializeField] public ScriptableApp scriptableApp;
//        [Space]
//        private IAppLauncher launcher;
//        private LauncerStatus launcerStatus;
//        [Space]
//        private IIziApp app;

//        private void Awake()
//        {
//#if UNITY_EDITOR
//            Debug.Log(GetType().AssemblyQualifiedName);
//            iziAppScheme.CleanupStaticFields();
//            scriptableApp.Dispose();
//#endif
//            this.enabled = true;

//            var launcher = iziAppScheme.GetLauncher();
//            var services = (launcher.Builder.Services as IIziServiceCollection);
//            services.AddSingleton(scriptableApp);
//            this.launcher = launcher;
//            launcerStatus = new(launcher);
//        }

//        private void Update()
//        {
//            while (launcher.Execute())
//            {
//                return;
//            }
//            this.enabled = false;
//            this.app = launcher.Complete();
//            scriptableApp.Initilize(app);
//            app.Run();
//#if UNITY_EDITOR
//            Debug.Log($"<color=lime>App Run()</color>", this);
//#endif
//        }
//    }

//    public class AppStateMachine
//    {
//        public IEnumerable<int> Itterate()
//        {
//            int stage = default;

//            while (!CreateBuilder())
//            {
//                yield return stage;
//            }
//            stage++;
//            yield return stage;
//        }
//        private void RunApp()
//        {
//            // create builder
//            // configure builder
//            // build app
//            // run app
//        }

//        private bool CreateBuilder()
//        {
//            return false;
//        }
//    }

//    public class LauncerStatus
//    {
//        private Status status;
//        private IAppLauncher launcher;
//        public LauncerStatus(IAppLauncher appLauncher)
//        {
//            this.launcher = appLauncher;
//        }
//    }
//}
