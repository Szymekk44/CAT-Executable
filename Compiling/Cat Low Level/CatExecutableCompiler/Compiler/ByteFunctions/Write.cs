namespace CatExecutableCompiler.Compiler.ByteFunctions
{
	public static class Write
	{
		public static void Byte(byte toWrite)
		{
			CompileToBytes.writer.Write(toWrite);
		}
		public static void Int(int toWrite)
		{
			CompileToBytes.writer.Write(toWrite);
		}
		public static void UShort(ushort toWrite)
		{
			CompileToBytes.writer.Write(toWrite);
		}
		public static void String(string toWrite)
		{
			CompileToBytes.writer.Write(toWrite);
		}
		public static void Long(long toWrite)
		{
			CompileToBytes.writer.Write(toWrite);
		}
	}
}
