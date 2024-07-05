using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Build.Locator;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Register the MSBuild instance
        MSBuildLocator.RegisterDefaults();

        // Load the solution file
        string solutionFilePath = "C:\\Users\\bugsbunny\\Pegasus\\PROJECTS\\USSD_SMS\\55.29_LiveUSSDBillPaymentsSenderService-main\\USSDBillPaymentsSender.sln";
        using var workspace = MSBuildWorkspace.Create();

        // Check for any diagnostics when opening the solution
        workspace.WorkspaceFailed += (sender, e) => Console.WriteLine($"Workspace failed: {e.Diagnostic.Message}");

        var solution = await workspace.OpenSolutionAsync(solutionFilePath);

        // Iterate through the projects in the solution
        foreach (var project in solution.Projects)
        {
            Console.WriteLine(project.Name);
            try
            {
                // Get the compilation for the project
                var compilation = await project.GetCompilationAsync();
                Console.WriteLine($"*** compilation: {compilation}");
                if (compilation == null)
                {
                    Console.WriteLine($"Compilation for project '{project.Name}' is null.");
                    continue;
                }

                // Iterate through the symbols in the compilation
                foreach (var symbol in compilation.GlobalNamespace.GetMembers())
                {
                    Console.WriteLine($"*** symbol: {symbol}");
                    // Check if the symbol is a method
                    if (symbol is INamedTypeSymbol methodSymbol)
                    {
                        // Get the method signature
                        //var methodSignature = $"{methodSymbol.ReturnType} {methodSymbol.Name}({string.Join(", ", methodSymbol.Parameters.Select(p => p.Type.ToString()))})";

                        // Do something with the method signature, e.g., print it to the console
                        //Console.WriteLine("sig: " + methodSignature);

                        int count = 0;
                        foreach (var item in methodSymbol.GetMembers())
                        {
                            Console.WriteLine($"\t{count}:{item}");
                            count++;
                        }
                       

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process project '{project.Name}': {ex.Message}");
            }
        }
    }
}
