using CatExecutableCompiler.Compiler;
using CatExecutableCompiler.Compiler.CustomConsole;

namespace CatExecutableCompiler 
{
	public class Program
	{
		public static string? output;
		static void Main(string[] args)
		{
            ConsoleActions.SetForegroundColor(ConsoleColor.White);
            if (args.Length < 1)
			{
                ConsoleActions.WriteLine("No file to compile.");
				return;
			}
			output = args[0].Replace(".cll", ".ce");
			ConsoleActions.WriteLine("Compiling Cat Low Level script to Cat Executable");
			CLLCompiler.Compile(args[0]);
			Console.ReadKey();
		}
	}
}