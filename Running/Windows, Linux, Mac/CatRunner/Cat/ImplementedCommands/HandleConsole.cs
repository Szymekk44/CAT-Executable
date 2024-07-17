using CatRunner.Cat.Variables;

namespace CatRunner.Cat.ImplementedCommands
{
	public static class HandleConsole
	{
		public static void Handle(BinaryReader reader, Executor executor)
		{
			byte action = reader.ReadByte();
			switch (action)
			{
				case 0: //Write
					{
						string toWrite = Convert.ToString(ReadVariable.Read(reader, executor))!;
						string[] splitted = toWrite.Split(@"\n");
						for (int i = 0; i < splitted.Length; i++)
						{
							executor.runner.ConsoleWrite(splitted[i]);
							if (i > 0)
							{
								executor.runner.ConsoleNewLine();
							}
						}
					}
					break;
			}
		}
	}
}
