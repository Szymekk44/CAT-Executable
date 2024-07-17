using CatExecutable.Compilator.LibraryManager;

namespace CatExecutable.Compilator
{
	public static class ReplaceFunctions
	{
		public static bool AddWhitespaces = true;
		public static string Replace()
		{
			string CLLCode = "";
			List<LowLevelFunction> LLF = new List<LowLevelFunction>();
			for (int i = 0; i < RasCompiler.tokens.Length; i++)
			{
				bool shouldProcess = true;
				switch (RasCompiler.tokens[i].Type)
				{
					case rasTokenType.IDENT:
						{
							if (RasCompiler.tokens[i + 1].Type != rasTokenType.DOT)
								shouldProcess = false;
							if (shouldProcess && !RasCompiler.modules.ContainsKey(RasCompiler.tokens[i].Value!))
								shouldProcess = false;

							if (shouldProcess && !RasCompiler.modules[RasCompiler.tokens[i].Value!].Functions.ContainsKey(RasCompiler.tokens[i + 2].Value!))
								shouldProcess = false;

							if (shouldProcess)
							{
								CLLCode += RasCompiler.modules[RasCompiler.tokens[i].Value!].Functions[RasCompiler.tokens[i + 2].Value!].CLLFunction;
								LLF = RasCompiler.modules[RasCompiler.tokens[i].Value!].Functions[RasCompiler.tokens[i + 2].Value!].Functions;
								i += 2;
							}
							else
								CLLCode += RasCompiler.tokens[i].Value + " ";
						}
						break;
					case rasTokenType.STRING:
						{
							CLLCode += "\"" + RasCompiler.tokens[i].Value + "\"";
						}
						break;
					case rasTokenType.STARTPAREN:
					case rasTokenType.ENDPAREN:
						CLLCode += RasCompiler.tokens[i].Value;
						break;
					case rasTokenType.STARTBRACKET:
					case rasTokenType.ENDBRACKET:
						{
							CLLCode += "\n" + RasCompiler.tokens[i].Value + "\n";
						}
						break;
					case rasTokenType.SEMI:
						{
							CLLCode += RasCompiler.tokens[i].Value + "\n";
							if (LLF != null)
							{
								for (int j = 0; j < LLF.Count; j++)
								{
									CLLCode += LLF[j].Funct + "\n";
								}
								LLF = new List<LowLevelFunction>();
							}
						}
						break;
					default:
						{
							CLLCode += RasCompiler.tokens[i].Value + " ";
						}
						break;
				}
			}
			return CLLCode;
		}
	}
}
