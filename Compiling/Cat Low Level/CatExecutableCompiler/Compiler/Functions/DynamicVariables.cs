using CatExecutableCompiler.Compiler.ByteFunctions;
using CatExecutableCompiler.Compiler.CustomConsole;

namespace CatExecutableCompiler.Compiler.Functions
{
    public static class DynamicVariables
    {
        public static void Handle(VarType type, ref int i)
        {
            if (CLLCompiler.CurrentVariables.ContainsKey(CLLCompiler.Commands![i].tokens![0].Value))
            {
                ConsoleActions.CompilationError($"Variable {CLLCompiler.Commands[i].tokens![0].Value} already exists!");
            }
            int token = 0;
            VarType varType;
            varType = SaveVariable.SaveVariableCreation(CLLCompiler.Commands[i].tokens!, ref token, CLLCompiler.Commands![i].value!);

            if (CLLCompiler.Commands[i].tokens!.Count > token)
            {

                Write.Byte(30);
                SaveMath.Save(CLLCompiler.Commands[i].tokens![token].Value!);
                token++;
                SaveVariable.Save(CLLCompiler.Commands[i].tokens!, ref token, varType);
            }
            else
                Write.Byte(31);

            CLLCompiler.LocalScopes[CLLCompiler.LocalScopes.Count - 1].Add(CLLCompiler.Commands[i].tokens![0].Value, new Variable { Type = type });
        }
        public static void Clean()
        {
            foreach (var item in CLLCompiler.LocalScopes[CLLCompiler.LocalScopes.Count - 1])
            {
                Console.WriteLine("Cleaning: " + item.Key);
                if (CLLCompiler.CurrentVariables.ContainsKey(item.Key))
                {
                    CLLCompiler.CurrentVariables.Remove(item.Key);
                }
            }
            CLLCompiler.LocalScopes.RemoveAt(CLLCompiler.LocalScopes.Count - 1);
        }
    }
}
