using System;

namespace IziHardGames.DependencyInjection.Attributes
{
    /// <summary>
    /// Не добаввляет класс в DI контейнер через рефлексию
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExcludeAutoDIAttribute : Attribute
    {

    }
}
