namespace CatRunner.Cat
{
	public class ConsoleRunner : Runner
	{
		public override void ConsoleWrite(string content)
		{
			Console.Write(content);
		}
		public override void ConsoleNewLine()
		{
			Console.WriteLine();
		}
	}
}
