#nullable enable
//using System;
//using System.Linq;
//using IziHardGames.AppConstructor;
//using IziHardGames.ApplicationLevel;
//using IziHardGames.Apps.Abstractions.Lib;
//using IziHardGames.CommonDomain.Contracts;
//using IziHardGames.CommonDomain.Events;
using System;
using System.Collections.Generic;
using IziHardGames.UnityApps.Contracts.Apps;

namespace IziHardGames.Apps.ForUnity
{
    public class BuildContinuation
    {
        private IIziApp? app;
        private Queue<Action<IIziApp>> continuations = new Queue<Action<IIziApp>>();

        internal void Resolve(IIziApp app)
        {
            this.app = app;
            while (continuations.TryDequeue(out var cont))
            {
                cont.Invoke(app);
            }
        }

        public void Then(Action<IIziApp> continuation)
        {
            this.continuations.Enqueue(continuation);
        }

        public void Then<TApp>(Action<TApp> continuation) where TApp : class, IIziApp
        {
            Action<IIziApp> action = (x) => continuation((x as TApp) ?? throw new InvalidCastException($"Actual: {x.GetType().AssemblyQualifiedName}. Expected: {typeof(TApp).AssemblyQualifiedName}"));
            this.continuations.Enqueue(action);
        }
    }
}