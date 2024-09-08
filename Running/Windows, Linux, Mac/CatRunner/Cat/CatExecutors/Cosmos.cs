using CatRunner.Cat.ImplementedCommands;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CatRunner.Cat.CatExecutors
{
    public class Cosmos : Executor
    {
        public override void Execute()
        {
            if (Finished) return; //Make sure to stop any processes using this CAT runner!

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
                case 33: //Destroy localscopes
                    {

                    }
                    break;
                case 34: //Last jump
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
    }
}

