//using System;
//using System.Collections.Generic;
//using IziHardGames.Apps.Abstractions.Lib;
//using IziHardGames.Apps.Contracts;
//using UnityEngine;

//namespace IziHardGames.AppConstructor
//{
//    public sealed class IziAppLauncher : IAppLauncher
//    {
//        private readonly AppStateMachine appStateMachine = new AppStateMachine();

//        private IEnumerator<int> itterator;
//        private IziAppBuilder builder;
//        private IIziAppScheme scheme;
//        private IIziApp app;
//        public IIziApp App => app;
//        public IIziAppBuilder Builder => builder;

//        public IziAppLauncher(IIziAppScheme scheme)
//        {
//            this.scheme = scheme;
//            itterator = Itterate();
//            this.builder = scheme.GetBuilder<IziAppBuilder>(scheme);
//        }
//        public int GetStage() => itterator.Current;
//        public bool Execute()
//        {
//            return itterator.MoveNext();
//        }
//        public IEnumerator<int> Itterate()
//        {
//            try
//            {
//                var value = itterator.Current;
//                Debug.Log("Itterate 0");
//            }
//            catch (Exception ex)
//            {
//                Debug.LogException(ex);
//            }
//            yield return 0;
//            Debug.Log("Itterate 0 Finish");

//            while (builder.InitilizeInner())
//            {
//                Debug.Log("Itterate 1");
//                yield return 1;
//            }
//            Debug.Log("Itterate 1 Finish");
//            while (builder.InitilizeServices())
//            {
//                yield return 2;
//            }
//            Debug.Log("Itterate 2 Finish");
//            yield return 3;
//            yield return int.MaxValue;

//            //if (!status.isLoaded)
//            //{
//            //    if (!iziAppScheme?.KeepItterateLoading() ?? false)
//            //    {
//            //        iziAppScheme!.StartupEnd();
//            //        status.isLoaded = true;
//            //    }
//            //}
//            //else if (!status.isResolvedDependecies)
//            //{
//            //    if (!iziAppScheme?.KeepResolveDependecies() ?? false)
//            //    {
//            //        status.isResolvedDependecies = true;
//            //    }
//            //}
//            //else
//            //{
//            //}
//            this.app = builder.Build();
//            Debug.Log("App builded");
//        }


//        public void SetScheme(IIziAppScheme scheme)
//        {
//            this.scheme = scheme;
//        }

//        public IIziApp Complete()
//        {
//            return this.app;
//        }
//    }
//}
