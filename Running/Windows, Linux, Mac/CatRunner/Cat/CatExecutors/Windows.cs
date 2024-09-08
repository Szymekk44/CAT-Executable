using CatRunner.Cat.CatFunctions;
using CatRunner.Cat.ImplementedCommands;
using CatRunner.Cat.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CatRunner.Cat.CatExecutors
{
    public class Windows : Executor
    {
        public override void Execute()
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
                            LocalScopes.Add(new Dictionary<string, Variable>());
                        }
                        break;
                    case 33: //Destroy localscopes
                        {
                            DynamicVariables.Clean(this);
                        }
                        break;
                    case 34: //Jump to Last jump
                        {
                            reader.BaseStream.Position = jumps[jumps.Count - 1] + 8;
                        }
                        break;
                    case 2: //Variable
                        {
                            HandleVariable.Handle(reader, this);
                        }
                        break;
                    case 3: //Create
                        {
                            HandleCreation.Handle(reader, this);
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
