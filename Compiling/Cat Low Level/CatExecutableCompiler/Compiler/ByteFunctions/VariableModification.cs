using CatExecutableCompiler.Compiler.CustomConsole;
using CatExecutableCompiler.Compiler.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatExecutableCompiler.Compiler.ByteFunctions
{
    public static class VariableModification
    {
        public static void HandleModification(ref int i)
        {
            CompileToBytes.writer.Write((byte)2);
            CompileToBytes.writer.Write((byte)0);
            CompileToBytes.writer.Write(CLLCompiler.Commands[i].value);
            int token = 0;
            SaveMath.Save(CLLCompiler.Commands[i].tokens[token].Value);
            token++;
            SaveVariable.Save(CLLCompiler.Commands[i].tokens, ref token, CLLCompiler.CurrentVariables[CLLCompiler.Commands[i].value].Type, true);
        }
    }
}
