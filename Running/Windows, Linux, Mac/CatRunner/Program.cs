using CatRunner.Cat;

namespace CatRunner
{
	internal class CatRunner
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Nothing to execute!");
				return;
			}
			Executor executor = new Executor();
			executor.Setup(args[0]);
		}
	}
}