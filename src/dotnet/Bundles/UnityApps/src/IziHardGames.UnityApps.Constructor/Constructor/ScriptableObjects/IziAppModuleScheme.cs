using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using IziHardGames.Apps.Abstractions.Lib;
using UnityEngine;
using static IziHardGames.Apps.Abstractions.ForUnity.Presets.ConstantsForUnityEditorMenus;


namespace IziHardGames.AppConstructor
{
    [CreateAssetMenu(fileName = nameof(IziAppModuleScheme), menuName = NAME_ROOT_MENU_NAME + "/" + NAME_MENU_CATEGORY_MODULES + "/" + nameof(IziAppModuleScheme))]
    public class IziAppModuleScheme : ScriptableObject
    {
        [SerializeField]
        public int orderToStartup;
        public List<IziModuleBind> binds = new List<IziModuleBind>();
        internal void Begin(IziAppModuled app)
        {
            foreach (var bind in binds)
            {
                app.PutItem(bind.GuidAsString, bind);
                bind.LoadModuleBegin(app);
            }
        }
        internal void End()
        {
            foreach (var bind in binds)
            {
                bind.LoadModuleEnd();
                bind.status.SetAsLoaded();
            }
        }
        internal void CleanupStaticFields()
        {
            foreach (var bind in binds)
            {
                bind.CleanupStaticFields();
            }
        }

        internal void ItterateLoading()
        {
            foreach (var bind in binds)
            {
                if (!bind.status.Loaded)
                {
                    bind.ItterateLoading();
                }
            }
        }
        internal bool IsLoadCompleted()
        {
            foreach (var bind in binds)
            {
                if (!bind.IsLoadCompleted()) return false;
            }
            return true;
        }

        internal bool ResolveDependecies()
        {
            foreach (var bind in binds)
            {
                if (!bind.status.Resolved && bind.ResolveDependecies())
                {
                    bind.status.SetAsResolved();
                }
            }
            return binds.All(x => x.status.Resolved);
        }

        public IAppLauncher GetLauncher()
        {

            return default;
        }
    }
}
