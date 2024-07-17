namespace CatExecutableCompiler.Compiler
{
	public class Variable
	{
		public object Value;
		public VarType Type;
	}
	public enum VarType { Bool, Byte, Short, Ushort, Int, Uint, Long, Ulong, Float, Double, String }
}
