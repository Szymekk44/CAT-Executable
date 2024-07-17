using CatExecutableCompiler.Compiler.ByteFunctions;

namespace CatExecutableCompiler.Compiler
{
	public static class CompileToBytes
	{
		public static BinaryWriter writer;
		public static void Compile()
		{
			writer = new BinaryWriter(File.Open(Program.output, FileMode.Create));
			Header.WriteHeader();
			for (int i = 0; i < CLLCompiler.Commands!.Count; i++)
			{
				switch (CLLCompiler.Commands[i].value)
				{
					case "CW":
						{
							CW.Save(ref i);
						}
						break;
					case "void":
						{
							CLLCompiler.Voids[CLLCompiler.Commands[i].tokens![0].Value].voidPosition = writer.BaseStream.Position;
						}
						break;
				}
			}
			Write.Byte(Byte.MaxValue);
			HandleVoidRequests();
			writer.Flush();
		}
		public static void HandleVoidRequests()
		{
			foreach (var CurrentVoid in CLLCompiler.Voids)
			{
				foreach (var Position in CurrentVoid.Value.requestPositions)
				{
					writer.BaseStream.Position = (long)Position;
					writer.Write((long)CurrentVoid.Value.voidPosition);
				}
			}
		}
	}
}
