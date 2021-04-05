using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            

            var text = System.IO.File.ReadAllText(@"File/Code.cs");
            var tree = CSharpSyntaxTree.ParseText(text);
            var root = tree.GetRoot();


            var directives = root.DescendantNodes().OfType<UsingDirectiveSyntax>().ToList();
            var objectCreationExprs = root.DescendantNodes().OfType<ObjectCreationExpressionSyntax>().ToList();
            string namespacesHeader = "<< === NAMESPACES === >>\n";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (namespacesHeader.Length / 2)) + "}", namespacesHeader));
            foreach (var item in directives)
            {
                var identifierNameList = item.DescendantNodes().OfType<IdentifierNameSyntax>().ToList();

                Console.Write("\t\t\t");
                foreach (var identifierName in identifierNameList)
                {
                    if(identifierNameList.Count != 1 && identifierName == identifierNameList.Last())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(" " + identifierName);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(identifierName);
                    }
                    
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }


            string fieldsHeader = "<< === FIELDS === >>\n";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (fieldsHeader.Length / 2)) + "}", fieldsHeader));
            foreach (var item in objectCreationExprs)
            {
                var variableDecs = item.Ancestors().OfType<VariableDeclaratorSyntax>().ToList();
                var identifierNames = item.DescendantNodes().OfType<IdentifierNameSyntax>().ToList();

                var vi = variableDecs.Zip(identifierNames, (v, i) => new { variableDec = v, identifierName = i });

                foreach (var v in vi)
                {
                    Console.Write("\t\t\t" + "Variable Name: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(v.variableDec.Identifier);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ==> Identifier Name: ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(v.identifierName);
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ReadKey();
        }
    }
}
