using CatExecutableCompiler.Compiler.ByteFunctions;
using CatExecutableCompiler.Compiler.CustomConsole;
using CatExecutableCompiler.Compiler.Functions;
using System.Linq;

namespace CatExecutableCompiler.Compiler
{
    public static class CompileToBytes
    {
        public static BinaryWriter writer;
        static bool insideVoid;
        static string functionName;
        static int insideBrackets;
        public static void Compile()
        {
            writer = new BinaryWriter(File.Open(Program.output, FileMode.Create));
            Header.WriteHeader();
            for (int i = 0; i < CLLCompiler.Commands!.Count; i++)
            {
                if (CLLCompiler.GlobalVariables.Contains(i))
                    continue;
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
                            functionName = CLLCompiler.Commands[i].tokens![0].Value;
                            insideVoid = true;
                            CLLCompiler.LocalScopes.Add(new Dictionary<string, Variable>()); //Simulate localscopes
                        }
                        break;
                    case "string":
                        {
                            DynamicVariables.Handle(VarType.String, ref i);
                        }
                        break;
                    default:
                        {
                            if (CLLCompiler.CurrentVariables.ContainsKey(CLLCompiler.Commands[i].value))
                            {
                                VariableModification.HandleModification(ref i);
                            }
                            else
                            {
                                if (CLLCompiler.Commands[i].value == "}")
                                {
                                    if (insideVoid)
                                    {
                                        insideBrackets--;

                                        if (insideBrackets == 0)
                                        {
                                            insideVoid = false;
                                            if (functionName == "Main")
                                            {
                                                Write.Byte(Byte.MaxValue);
                                            }
                                            else
                                            {
                                                Write.Byte(33);
                                                Write.Byte(34);
                                            }
                                            DynamicVariables.Clean();
                                        }
                                    }
                                }
                                else if (CLLCompiler.Commands[i].value == "{")
                                {
                                    if (insideVoid)
                                    {
                                        insideBrackets++;
                                    }
                                }
                                else
                                    ConsoleActions.CompilationError(CLLCompiler.Commands[i].value + " does not exist in current context.");
                            }
                        }
                        break;
                }
            }
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
                    Write.Byte(25);
                    CLLCompiler.Voids[CLLCompiler.Commands[token].value].requestPositions.Add(CompileToBytes.writer.BaseStream.Length);
                    Write.Long(0); //Value to overwrite
                    //DynamicVariables.TempClean();
                    break;
            }
        }
    }
}
