using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace CodeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var text = System.IO.File.ReadAllText(@"Code.txt");
            Console.WriteLine(text);
            var tree = CSharpSyntaxTree.ParseText(text);
            var root = tree.GetRoot();

            var directives = root.DescendantNodes().OfType<UsingDirectiveSyntax>().ToList();

            foreach (var item in directives)
            {
                Console.WriteLine(item);
            }   
        }
    }
}
