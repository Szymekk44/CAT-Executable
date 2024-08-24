namespace CatExecutableCompiler.Compiler.Functions
{
	public static class SaveMath
	{
		public static void Save(string math)
		{
			switch (math)
			{
				case "=":
					CompileToBytes.writer.Write((byte)5); break;
				case "+":
					CompileToBytes.writer.Write((byte)6); break;
				case "-":
					CompileToBytes.writer.Write((byte)7); break;
				case "*":
					CompileToBytes.writer.Write((byte)8); break;
				case "/":
					CompileToBytes.writer.Write((byte)9); break;
				case "%":
					CompileToBytes.writer.Write((byte)10); break;
                case "(":
                    CompileToBytes.writer.Write((byte)28); break;
                case ")":
                    CompileToBytes.writer.Write((byte)29); break;
                case "++":
					CompileToBytes.writer.Write((byte)24);
					CompileToBytes.writer.Write((byte)6);
					CompileToBytes.writer.Write((byte)6); break;
				case "--":
					CompileToBytes.writer.Write((byte)24);
					CompileToBytes.writer.Write((byte)7);
					CompileToBytes.writer.Write((byte)7); break;
				case "+=":
					CompileToBytes.writer.Write((byte)24);
					CompileToBytes.writer.Write((byte)6);
					CompileToBytes.writer.Write((byte)5); break;
				case "-=":
					CompileToBytes.writer.Write((byte)24);
					CompileToBytes.writer.Write((byte)7);
					CompileToBytes.writer.Write((byte)5); break;
				case "*=":
					CompileToBytes.writer.Write((byte)24);
					CompileToBytes.writer.Write((byte)8);
					CompileToBytes.writer.Write((byte)5); break;
				case "/=":
					CompileToBytes.writer.Write((byte)24);
					CompileToBytes.writer.Write((byte)9);
					CompileToBytes.writer.Write((byte)5); break;
				case "%=":
					CompileToBytes.writer.Write((byte)24);
					CompileToBytes.writer.Write((byte)10);
					CompileToBytes.writer.Write((byte)5); break;
            }
		}
	}
}
