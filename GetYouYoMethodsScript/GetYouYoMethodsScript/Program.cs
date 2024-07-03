/* 
 this is a script to return the method signatures of a complex programme
 I use this to learn how to use top-level statement in C#. 
This is a feature introduced in C# 9.0
 */


using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var projectPath = @"C:\Users\bugsbunny\Pegasus\New folder\55.29_UssdPaymentSender272-main\UssdPaymentSenderService\UssdPaymentSenderService.csproj";
var compilation = CSharpCompilation.Create(projectPath);

foreach (var tree in compilation.SyntaxTrees)
{
    var root = tree.GetRoot();
    var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

    foreach (var method in methods)
    {
        Console.Write($"Method:\n {method.ReturnType} {method.Identifier.Text}");
        Console.WriteLine($"Parameters:");
        Console.WriteLine("(");
        foreach (var parameter in method.ParameterList.Parameters)
        {
            Console.Write($"{parameter}, ");
        }
        Console.WriteLine(")");
    }
}