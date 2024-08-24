using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatExecutableCompiler.Compiler.Functions
{
    public static class isVar
    {
        public static bool CanCast(string type)
        {
            if (type == "string" || type == "bool" || type == "byte" || type == "short" || type == "ushort" || type == "int" || type == "uint" || type == "long" || type == "ulong" || type == "float" || type == "double")
                return true;
            else
                return false;
        }
    }
}
