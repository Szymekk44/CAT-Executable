using CatExecutableCompiler.Compiler.Inspectors;
using CatExecutableCompiler.Compiler.Lexer;

namespace CatExecutableCompiler.Compiler
{
    public static class CLLCompiler
	{
		public static CLLToken[]? CLLTokens;
		public static List<CLLCommand>? Commands;
		public static List<int> GlobalVariables = new List<int>();
		public static Dictionary<string, VoidRequest> Voids = new Dictionary<string, VoidRequest>();
		public static Dictionary<string, Variable> CurrentVariables = new Dictionary<string, Variable>();
		public static List<RemoveCurrentVars> VarsToRemove = new List<RemoveCurrentVars>();

		public static void Compile(string path)
		{
			string data = File.ReadAllText(path);
			CLLLexer lexer = new CLLLexer(data);
			CLLTokens = lexer.Lex();
			Commands = GetCommands.Get();
			for (int i = 0; i < Commands.Count; i++)
			{
				Console.WriteLine("Found command: " + Commands[i].value);
				foreach (var item in Commands[i].tokens!)
				{
					Console.Write(" " + item.Value);
				}
				Console.WriteLine();
			}
			try
			{
				GlobalFinder.Find();
                FunctionFinder.Find();
                CompileToBytes.Compile();
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e);
			}
			Console.WriteLine("Done!");
		}

	}
}
