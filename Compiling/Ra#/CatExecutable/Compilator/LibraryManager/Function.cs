namespace CatExecutable.Compilator.LibraryManager
{
	public class Function
	{
		public string? CLLFunction { get; set; }
		public List<LowLevelFunction> Functions { get; set; } = new List<LowLevelFunction>();
	}
	public class LowLevelFunction
	{
		public string? Funct { get; set; }
	}
}
