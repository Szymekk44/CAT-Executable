using CatExecutable.Compilator.LibraryManager;

namespace CatExecutable.Compilator
{

	public static class RasCompiler
	{
		public static rasToken[] tokens;
		static public Dictionary<string, Module> modules = new Dictionary<string, Module>();
		/// <summary>
		/// Compilation form .ras (Ra#) to .cll (Cat Low Level)
		/// </summary>
		/// <param name="path"></param>
		public static void StartCompilation(string path)
		{
			string Data = File.ReadAllText(path);
			RasLexer lexer = new RasLexer(Data);
			tokens = lexer.Lex();
			GetLibraries();

			using (StreamWriter writer = new StreamWriter(path.Replace(".ras", ".cll")))
			{
				writer.WriteLine(ReplaceFunctions.Replace());
			}

		}
		/// <summary>
		/// Load libraries - modules
		/// </summary>
		public static void GetLibraries()
		{
			List<rasToken> newTokens = new List<rasToken>();
			for (int i = 0; i < tokens.Length; i++)
			{
				if (tokens[i].Type == rasTokenType.IDENT)
				{
					if (tokens[i].Value == "using")
					{
						string FullLibName = "";
						i++;
						while (tokens[i].Type != rasTokenType.SEMI)
						{
							FullLibName += tokens[i].Value;
							i++;
						}
						Console.WriteLine("Found using " + FullLibName);
						string ReadyPath = Program.LibrariesPath + FullLibName.Replace(".", Path.DirectorySeparatorChar + ".");
						LibraryLoader.Load(ReadyPath);
					}
					else
						newTokens.Add(tokens[i]);
				}
				else
				{
					newTokens.Add(tokens[i]);
				}
			}
			tokens = newTokens.ToArray();
		}
	}
}
