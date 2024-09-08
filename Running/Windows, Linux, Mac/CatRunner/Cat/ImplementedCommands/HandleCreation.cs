using CatRunner.Cat.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatRunner.Cat.ImplementedCommands
{
    public static class HandleCreation
    {
        public static void Handle(BinaryReader reader, Executor executor)
        {
            byte operation = reader.ReadByte();
            switch (operation)
            {
                case 2: //Variable
                    {
                        ReadVariable.CreateVariable(reader, executor, false, false);
                    }
                    break;
            }
        }
    }
}
