using CatExecutable.Compilator;

namespace CatExecutable
{
	public class Program
	{
		public static string? LibrariesPath;
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("No file to compile.");
				return;
			}
			LibrariesPath = args[0];
			DirectoryInfo parentDir = Directory.GetParent(Directory.GetParent(args[0])!.FullName)!;
			LibrariesPath = Path.Combine(parentDir.FullName, @"Libraries\");
			if (!Directory.Exists(LibrariesPath))
			{
				Console.WriteLine($"Path '{LibrariesPath}' is invalid; Please enter Libraries directory path...");
				LibrariesPath = Console.ReadLine();
			}

			Console.WriteLine("Compiling RaSharp script to CatLowLevel");
			RasCompiler.StartCompilation(args[0]);
			Console.ReadKey();
		}
	}
}