using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IziHardGames.Reflections
{
    public static class CsFile
    {
        public static IEnumerable<Type> FindDeclaredTypes(AppDomain appDomain, string csfilePath)
        {
            throw new System.NotImplementedException();
        }
        public static IEnumerable<Type> FindTypes(AppDomain appDomain, string csfilePath)
        {
            var results = FindTypes(csfilePath);
            var types = appDomain.GetAssemblies().SelectMany(x => x.GetTypes());
            foreach (var item in results)
            {
                var fullname = GetFullName(item);
                var type = types.FirstOrDefault(x => x.FullName == fullname);
                if (type != null) yield return type;
            }
        }
        /// <summary>
        /// Include interfaces. <see cref="InterfaceDeclarationSyntax"/> is inheritant of <see cref="TypeDeclarationSyntax"/>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<TypeDeclarationSyntax> FindTypes(string filePath)
        {
            string sourceCode = File.ReadAllText(filePath);

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = syntaxTree.GetRoot();
            return root.DescendantNodes().OfType<TypeDeclarationSyntax>();
        }
        public static IEnumerable<string> TypeName()
        {
            string filePath = "YourFilePath.cs"; // Path to your .cs file
            string sourceCode = File.ReadAllText(filePath);

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = syntaxTree.GetRoot();
            //Microsoft.CodeAnalysis.CSharp.Syntax.InterfaceDeclarationSyntax
            var types = root.DescendantNodes().OfType<TypeDeclarationSyntax>();

            foreach (var type in types)
            {
                Console.WriteLine($"Type: {type.Identifier}");
            }
            return types.Select(x => x.Identifier.Text);
        }
              
        static string GetFullName(TypeDeclarationSyntax typeDeclaration)
        {
            string typeName = typeDeclaration.Identifier.Text;
            string namespaceName = GetNamespace(typeDeclaration);

            return string.IsNullOrEmpty(namespaceName) ? typeName : $"{namespaceName}.{typeName}";
        }

        static string GetNamespace(SyntaxNode syntaxNode)
        {
            // Traverse up the syntax tree to find the namespace
            while (syntaxNode != null && !(syntaxNode is NamespaceDeclarationSyntax))
            {
                syntaxNode = syntaxNode.Parent;
            }

            if (syntaxNode is NamespaceDeclarationSyntax namespaceDeclaration)
            {
                return namespaceDeclaration.Name.ToString();
            }

            return string.Empty;
        }
    }
}
