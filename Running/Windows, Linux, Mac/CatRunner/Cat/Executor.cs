using CatRunner.Cat.ImplementedCommands;
using CatRunner.Cat.Variables;

namespace CatRunner.Cat
{
	public class Executor
	{
		public Runner? runner;
		public BinaryReader? reader;
		public Header? header;
		public Dictionary<string, Variable>? GlobalVariables;
		public bool Finished = false;
		public void Setup(string path)
		{
			reader = new BinaryReader(File.OpenRead(path));
			header = new Header(this);
			runner = new ConsoleRunner();
			header.Read(reader);
			Execute();
		}
		public void Execute()
		{
			while (!Finished)
			{
				byte currentCommand = reader!.ReadByte();
				switch (currentCommand)
				{
					case 1: //Console
						{
							HandleConsole.Handle(reader, this);
						}
						break;
					case 25: //jump
						{
							reader.BaseStream.Position = reader.ReadInt64();
						}
						break;
                    case 2: //Variable
                        {
							HandleVariable.Handle(reader, this);	
                        }
                        break;
                    case 255: //End
						{
							Finished = true;
						}
						break;
				}
                Thread.Sleep(100);
            }
			Console.WriteLine();
			Console.WriteLine("End of CAT");
			Console.ReadKey();
		}
	}
}
