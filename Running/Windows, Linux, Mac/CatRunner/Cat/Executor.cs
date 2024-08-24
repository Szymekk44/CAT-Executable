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
		public List<long> jumps = new List<long>();
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
					case 25: //Jump
						{
							jumps.Add(reader.BaseStream.Position);
							reader.BaseStream.Position = reader.ReadInt64();
						}
						break;
					case 33:
						{

						}
						break;
					case 34:
						{
                            reader.BaseStream.Position = jumps[jumps.Count - 1] + 8;
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
            }
			Console.WriteLine();
			Console.WriteLine("End of CAT");
			Console.ReadKey();
		}
	}
}
