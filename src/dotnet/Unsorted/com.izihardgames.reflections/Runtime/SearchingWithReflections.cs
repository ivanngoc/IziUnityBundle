using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace IziHardGames.Reflections
{
    public static class SearchingWithReflections
    {
        public static Type? FindTypesWithGuidAttribute(AppDomain domain, Guid guid)
        {
            return domain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => IsGuidAttribute(x, guid));
        }
        public static IEnumerable<Type> FindTypesWithInterface<TInterface>(AppDomain domain)
        {
            var type = typeof(TInterface);
            if (type.IsInterface) throw new ArgumentException("Not Interface: " + type.AssemblyQualifiedName);
            return domain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.GetInterfaces().Any(y => y == type));
        }

        public static bool HasInterfaceIncludeDefinition(Type type)
        {
            throw new System.NotImplementedException();
        }
        public static bool HasInterface(Type type)
        {
            throw new System.NotImplementedException();
        }

        public static bool IsGuidAttribute(Type x, Guid guid)
        {
            var atr = x.GetCustomAttributes(false).FirstOrDefault(x => x is GuidAttribute) as GuidAttribute;
            if (atr != null)
            {
                return Guid.Parse(atr.Value) == guid;
            }
            return false;
        }
    }
}
