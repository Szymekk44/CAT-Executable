using CatRunner.Cat;
using CatRunner.Cat.CatExecutors;

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
			Windows executor = new Windows();
			executor.Setup(args[0]);
		}
	}
}