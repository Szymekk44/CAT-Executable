namespace CatExecutable.Compilator.LibraryManager
{
	public static class LibraryLoader
	{
		public static void Load(string path)
		{
			//Get all modules
			string[] allDirs = Directory.GetDirectories(path);
			foreach (string dir in allDirs)
			{
				//Get all functions
				string[] files = Directory.GetFiles(dir);
				string moduleName = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1);
				if (moduleName.StartsWith('.'))
					continue;
				Console.WriteLine($"Loading module '{moduleName}'");
				Module module = new Module();
				foreach (string file in files)
				{
					string name = file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1);
					name = name.Substring(0, name.Length - 5); //remove .clib
					Function function = new Function();
					Console.WriteLine($"Loading function '{name}'");
					string Data = File.ReadAllText(file);
					clibLexer lexer = new clibLexer(Data);
					clibToken[] tokens = lexer.Lex();
					for (int i = 0; i < tokens.Length; i++)
					{
						switch (tokens[i].Type)
						{
							case clibTokenType.STARTCLLFUNCTION:
								{
									i++;
									string CLLfunction = "";
									while (tokens[i].Type != clibTokenType.ENDCLLFUNCTION)
									{
										CLLfunction += tokens[i].Value?.ToString();
										i++;
									}
									function.CLLFunction = CLLfunction;
								}
								break;
							default:
								{
									string CLLAdditionalFunction = "";
									bool stillContinue = true;
									while (tokens.Length > i && stillContinue)
									{
										switch (tokens[i].Type)
										{
											default:
												CLLAdditionalFunction += tokens[i].Value?.ToString();
												i++;
												break;
											case clibTokenType.SEMI:
												CLLAdditionalFunction += tokens[i].Value?.ToString();
												i++;
												stillContinue = false;
												break;
											case clibTokenType.STRING:
												CLLAdditionalFunction += "\"" + tokens[i].Value?.ToString() + "\"";
												i++;
												break;
										}
									}
									function.Functions.Add(new LowLevelFunction { Funct = CLLAdditionalFunction });
								}
								break;
						}
					}
					module.Functions.Add(name, function);
				}
				RasCompiler.modules.Add(moduleName, module);
			}
		}
	}
}
