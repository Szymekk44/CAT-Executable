using CatRunner.Cat.ImplementedCommands;
using CatRunner.Cat.Variables;
using System.IO;
using System.Collections.Generic;
using CatRunner.Cat.CatFunctions;


namespace CatRunner.Cat
{
    public class Executor
	{
		public Runner? runner;
		public BinaryReader? reader;
		public Header? header;
		public Dictionary<string, Variable>? GlobalVariables;
        public List<Dictionary<string, Variable>> LocalScopes = new List<Dictionary<string, Variable>>();
        public Dictionary<string, Variable> CurrentVariables = new Dictionary<string, Variable>();
        public List<RemoveCurrentVars> VarsToRemove = new List<RemoveCurrentVars>();
        public bool Finished = false;
		public List<long> jumps = new List<long>();
		public void Setup(string path)
		{
			reader = new BinaryReader(File.OpenRead(path));
			CreateRunner();
        }
        public void Setup(byte[] CatExecutable)
        {
            reader = new BinaryReader(new MemoryStream(CatExecutable));
			CreateRunner();
        }
		void CreateRunner()
		{
            header = new Header(this);
            runner = new ConsoleRunner();
            header.Read(reader);
            HandleTick();
        }
        public virtual void Execute()
		{
			
		}
		public virtual void HandleTick()
		{
			Execute();
        }
	}
}
