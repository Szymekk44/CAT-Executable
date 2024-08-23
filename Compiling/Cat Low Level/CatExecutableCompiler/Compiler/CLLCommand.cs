using CatExecutableCompiler.Compiler.Lexer;

namespace CatExecutableCompiler.Compiler
{
	public class CLLCommand
	{
		public string? value = "";
		public List<CLLToken>? tokens = new List<CLLToken>();
		public bool isDynamic;
		public CLLTag Tag;
	}
	public enum CLLTag { FunctionCalling, VariableName }
}
