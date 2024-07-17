using CatExecutableCompiler.Compiler;

namespace CatExecutableCompiler 
{
	public class Program
	{
		public static string? output;
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("No file to compile.");
				return;
			}
			output = args[0].Replace(".cll", ".ce");
			Console.WriteLine("Compiling Cat Low Level script to Cat Executable");
			CLLCompiler.Compile(args[0]);
			Console.ReadKey();
		}
	}
}