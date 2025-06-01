using System;
using System.Collections.Generic;
using System.Linq;
using IziHardGames.Apps.Abstractions.ForUnity.Presets;
using IziHardGames.Apps.Abstractions.Lib;
using IziHardGames.Apps.Contracts;
using IziHardGames.UnityApps.Contracts;
using IziHardGames.UnityApps.Contracts.Apps;


//using IziHardGames.Reflections;
using UnityEngine;
using static IziHardGames.Apps.Abstractions.ForUnity.Presets.ConstantsForUnityEditorMenus;


namespace IziHardGames.AppConstructor
{
    /*
    Services - singletons 100%
    Singletons
    Transients
    Scoped
    Objects
    */
    /// <summary>
    /// <see cref="IziAppModuleScheme"/>
    /// </summary>
    [CreateAssetMenu(fileName = nameof(IziAppScheme), menuName = NAME_ROOT_MENU_NAME + "/" + NAME_MENU_CATEGORY_MODULES + "/" + nameof(IziAppScheme))]
    public class IziAppScheme : ScriptableObject, IIziAppScheme
    {
        /*
         Переводим все в MVC like архитектуру:
         1. объявляем модели (дата типы)
         2. объявляем контроллеры (каждый контроллер оперирует набором моделей как в ECS)
         3. объявляем пайплайны: последовательность выполнения контроллеров

            Прототипы:
            Загрузка сцены - Пул Объектов - Триггер (создание) - запуска контроллера AI - получение AI команлы - выполнение AI команды - получение урона от игрока - проверка жизней - смерть
         */

        [SerializeField] private List<string> startups = new List<string>();
        [SerializeField] private List<IziAppModuleScheme> modules = new List<IziAppModuleScheme>();
        //[SerializeField] private List<ScriptableType> services = new List<ScriptableType>();
        //[SerializeField] private List<ScriptableDependecyContainer> containers = new List<ScriptableDependecyContainer>();
        [Space]
        [SerializeField] private List<UnityEngine.Object> controllers = new List<UnityEngine.Object>();
        [SerializeField] private List<UnityEngine.Object> models = new List<UnityEngine.Object>();
        [SerializeField] private List<UnityEngine.Object> pipelienes = new List<UnityEngine.Object>();

        private IziAppModuled? app;
        public IziAppModuled App => app;

        [ContextMenu("Izi Validate")]
        private void Validate()
        {
            modules = modules.OrderBy(x => x.orderToStartup).ToList();

            foreach (var guid in startups)
            {
                if (!Guid.TryParse(guid, out var correct))
                {
                    Debug.LogError($"Can't parse to guid: {guid}");
                }
            }
        }

        internal void StartapBegin()
        {
            IziAppModuled iziAppV2 = app = new IziAppModuled();

            foreach (var module in modules)
            {
                module.Begin(iziAppV2);
            }
        }
        internal void StartupEnd()
        {
            foreach (var module in modules)
            {
                module.End();
            }
        }
        public void CleanupStaticFields()
        {
            foreach (var item in modules)
            {
                item.CleanupStaticFields();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// <see langword="true"/> Itterating in progress - <br/>
        /// <see langword="false"/> - Itterating completed
        /// </returns>
        internal bool KeepItterateLoading()
        {
            foreach (var module in modules)
            {
                module.ItterateLoading();
            }

            foreach (var module in modules)
            {
                if (!module.IsLoadCompleted())
                {
                    return true;
                }
            }
            return false;
        }

        internal bool KeepResolveDependecies()
        {
            foreach (var module in modules)
            {
                if (!module.ResolveDependecies())
                {
                    return true;
                }
            }
            return false;
        }

        public IAppLauncher GetLauncher()
        {
            throw new System.NotImplementedException();
            //return new IziAppLauncher(this);
        }

        public T GetBuilder<T>(IIziAppScheme scheme) where T : class, IIziAppBuilder
        {
            if (typeof(T) == typeof(IziAppBuilder)) return new IziAppBuilder(null, scheme) as T;
            throw new NotImplementedException();
        }

        public IEnumerable<(Type, Type)> GetSingletons()
        {
            throw new System.NotImplementedException();
            //return containers.Where(x => x.type == EDependecyInjectionType.AppShared).Select(x => (x.typeAdvertized.Type, x.typeToImplement.Type));
        }

        public IEnumerable<(Type, Type)> GetTransients()
        {
            return Enumerable.Empty<(Type, Type)>();
        }

        public IEnumerable<Action<object>> GetStartups()
        {
            foreach (var guidStr in startups)
            {
                if (Guid.TryParse(guidStr, out var guid))
                {
                    //var type = SearchingWithReflections.FindTypesWithGuidAttribute(AppDomain.CurrentDomain, guid) ?? throw new NullReferenceException();
                    //var instance = (Activator.CreateInstance(type) as IStartup) ?? throw new NullReferenceException();
                    //yield return instance.ConfigureStartup;
                    throw new System.NotImplementedException();
                    yield break;
                }
                else
                {
                    throw new FormatException(guidStr);
                }
            }
        }

        public IEnumerable<Type> GetServices()
        {
            throw new System.NotImplementedException();
            //foreach (var item in services)
            //{
            //    yield return item.SerilizedType.Type;
            //}
        }
    }
}
