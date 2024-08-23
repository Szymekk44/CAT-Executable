using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatExecutableCompiler.Compiler.Inspectors
{
    public static class FunctionFinder
    {
        public static void Find()
        {
            int CurrentIndexInBrackets = 0;
            for (int i = 0; i < CLLCompiler.Commands!.Count; i++)
            {
                switch (CLLCompiler.Commands[i].value)
                {
                    case "void":
                    case "{":
                    case "}":
                    case "string":
                    case "bool":
                    case "byte":
                    case "short":
                    case "ushort":
                    case "int":
                    case "uint":
                    case "long":
                    case "ulong":
                    case "float":
                    case "double":
                        break;
                    default:
                        if (CLLCompiler.Voids.ContainsKey(CLLCompiler.Commands[i].value))
                        {
                            CLLCompiler.Commands[i].isDynamic = true;
                            CLLCompiler.Commands[i].Tag = CLLTag.FunctionCalling;
                        }
                        break;
                }
            }
        }
    }
}
