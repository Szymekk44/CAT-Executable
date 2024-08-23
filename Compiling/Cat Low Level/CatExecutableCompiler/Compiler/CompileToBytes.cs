using CatExecutableCompiler.Compiler.ByteFunctions;
using System.Linq;

namespace CatExecutableCompiler.Compiler
{
    public static class CompileToBytes
    {
        public static BinaryWriter writer;
        public static void Compile()
        {
            writer = new BinaryWriter(File.Open(Program.output, FileMode.Create));
            Header.WriteHeader();
            for (int i = 0; i < CLLCompiler.Commands!.Count; i++)
            {
                if (CLLCompiler.Commands[i].isDynamic)
                {
                    HandleDynamic(i);
                    continue;
                }
                switch (CLLCompiler.Commands[i].value)
                {
                    case "CW":
                        {
                            CW.Save(ref i);
                        }
                        break;
                    case "void":
                        {
                            CLLCompiler.Voids[CLLCompiler.Commands[i].tokens![0].Value].voidPosition = writer.BaseStream.Position;
                        }
                        break;
                    default:
                        {
                            if (CLLCompiler.CurrentVariables.ContainsKey(CLLCompiler.Commands[i].value))
                            {
                               VariableModification.HandleModification(ref i);
                            }
                        }
                        break;
                }
            }
            Write.Byte(Byte.MaxValue);
            HandleVoidRequests();
            writer.Flush();
        }
        public static void HandleVoidRequests()
        {
            foreach (var CurrentVoid in CLLCompiler.Voids)
            {
                foreach (var Position in CurrentVoid.Value.requestPositions)
                {
                    writer.BaseStream.Position = (long)Position;
                    writer.Write((long)CurrentVoid.Value.voidPosition);
                }
            }
        }
        public static void HandleDynamic(int token)
        {
            switch (CLLCompiler.Commands[token].Tag)
            {
                case CLLTag.FunctionCalling:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Found!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Write.Byte(25);
                    CLLCompiler.Voids[CLLCompiler.Commands[token].value].requestPositions.Add(CompileToBytes.writer.BaseStream.Length);
                    Write.Long(0); //Value to overwrite
                    break;
            }
        }
    }
}
