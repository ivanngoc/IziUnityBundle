#nullable enable
//using System;
//using System.Linq;
//using IziHardGames.AppConstructor;
//using IziHardGames.ApplicationLevel;
//using IziHardGames.Apps.Abstractions.Lib;
//using IziHardGames.CommonDomain.Contracts;
//using IziHardGames.CommonDomain.Events;
using UnityEngine;

namespace IziHardGames.Apps.ForUnity
{
    public abstract class AppBuilderMono : MonoBehaviour
    {
        public abstract BuildContinuation Build();
    }
}