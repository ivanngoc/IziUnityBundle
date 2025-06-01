using System;
using System.Runtime.InteropServices;
using IziHardGames.UnityApps.Attributes.Searching;

namespace IziHardGames.UnityApps.Attributes.DependencyInjection
{
    /// <summary>
    /// Use to inject value into UnityEngine.MonoBehaviour fields
    /// </summary>
    [Guid("ba1ca7c9-805c-4ce4-add9-ea996163d9a9")]
    public class IziInjectAttribute : Attribute
    {
        public ESearchType SearchType { get; private set; }
        public Type? InjectType { get; private set; }

        public IziInjectAttribute() : base()
        {
            SearchType = ESearchType.None;
        }
        public IziInjectAttribute(ESearchType searchType)
        {
            SearchType = searchType;
        }

        public IziInjectAttribute(Type injectType)
        {
            InjectType = injectType;
        }
    }
}
