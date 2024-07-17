namespace CatExecutableCompiler.Compiler
{
	public static class GlobalFinder
	{
		public static void Find()
		{
			int CurrentIndexInBrackets = 0;
			for (int i = 0; i < CLLCompiler.Commands!.Count; i++)
			{
				switch (CLLCompiler.Commands[i].value)
				{
					case "void":
						CLLCompiler.Voids.Add(CLLCompiler.Commands[i].tokens![0].Value, new VoidRequest());
						CurrentIndexInBrackets = 0;
						break;
					case "{":
						CurrentIndexInBrackets++;
						break;
					case "}":
						CurrentIndexInBrackets--;
						break;
					case "string":
					case "bool":
					case "byte":
					case "short":
					case "ushort":
					case "int":
					case "uint":
					case "long":
					case "ulong":
					case "float":
					case "double":
						if (CurrentIndexInBrackets == 0) //Global
							CLLCompiler.GlobalVariables.Add(i);
						break;
					default:
						if (CurrentIndexInBrackets == 0)
						{
							throw new Exception($"'{CLLCompiler.Commands[i].value}' does not exist in the current context. Make sure you call it in the method.");
						}
						break;
				}
			}
		}
	}
}
