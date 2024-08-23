using CatExecutableCompiler.Compiler.Configs;
using CatExecutableCompiler.Compiler.Functions;

namespace CatExecutableCompiler.Compiler.ByteFunctions
{
	public static class Header
	{
		public static int CATVersion = 1;
		public static void WriteHeader()
		{
			Write.Byte(127);
			Write.Byte(67);
			Write.Byte(65);
			Write.Byte(84);
			Write.Int(CAT.CATVersion);
			Write.Byte(Build.PermissionLevelNeeded);
			Write.String(Build.AppName);
			Write.String(Build.Description);
			SaveGlobalVariables();
			Write.Byte(25);
			CLLCompiler.Voids["Main"].requestPositions.Add(CompileToBytes.writer.BaseStream.Length);
			Write.Long(0);
		}

		public static void SaveGlobalVariables()
		{
			Write.Long(CLLCompiler.GlobalVariables.Count);
			for (int i = 0; i < CLLCompiler.GlobalVariables.Count; i++)
			{
				int currentIndex = CLLCompiler.GlobalVariables[i];
				Console.WriteLine("Findng global variable: " + CLLCompiler.Commands![currentIndex].value);

				int token = 0;
				VarType varType;
				varType = SaveVariable.SaveVariableCreation(CLLCompiler.Commands[currentIndex].tokens!, ref token, CLLCompiler.Commands![currentIndex].value!);
				Console.WriteLine(CLLCompiler.Commands[currentIndex].tokens!.Count + " but " + token);
                for (int j = 0;  j < CLLCompiler.Commands[currentIndex].tokens!.Count; j++)
                {
					Console.WriteLine(CLLCompiler.Commands[currentIndex].tokens![j].Value);
                }
                if (CLLCompiler.Commands[currentIndex].tokens!.Count > token)
				{
					
					Write.Byte(30);
					SaveMath.Save(CLLCompiler.Commands[currentIndex].tokens[token].Value!);
					token++;
					SaveVariable.Save(CLLCompiler.Commands[currentIndex].tokens!, ref token, varType);
				}
				else
					Write.Byte(31);


			}
		}
	}
}
