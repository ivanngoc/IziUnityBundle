﻿using System;
using Microsoft.CodeAnalysis;

namespace IziHardGames.SourceGenerators.UnitySG
{
    [Generator]
    public class MySourceGenerator : IIncrementalGenerator
    {
        //public void Execute(GeneratorExecutionContext context)
        //{
        //    // определяем генерируемый код
        //    var code = @"
        //    namespace Metanit
        //    {
        //        public static class Welcome 
        //        {
        //            public const string Name = ""Eugene"";
        //            public static void Print() => Console.WriteLine($""Hello {Name}!"");
        //        }
        //    }";
        //    context.AddSource("metanit.welcome.generated.cs", code);
        //}
        public void Initialize(GeneratorInitializationContext context)
        {
            // инициализация не нужна
        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
