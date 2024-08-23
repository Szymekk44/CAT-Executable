using CatExecutableCompiler.Compiler.Functions;

namespace CatExecutableCompiler.Compiler.ByteFunctions
{
	public static class CW
	{
		public static void Save(ref int i)
		{
			CompileToBytes.writer.Write((byte)1);
			CompileToBytes.writer.Write((byte)0);
			int currentToken = 0;
			SaveVariable.Save(CLLCompiler.Commands[i].tokens, ref currentToken, VarType.String, true);
		}
	}
}
