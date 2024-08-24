using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatExecutableCompiler.Compiler.CustomConsole
{
    public static class ConsoleActions
    {
        public static void Write(string message)
        {
            Console.Write(message);
        }
        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public static void SetForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        public static void CompilationError(string message)
        {
            SetForegroundColor(ConsoleColor.White);
            Write("[");
            SetForegroundColor(ConsoleColor.Red);
            Write("Error");
            SetForegroundColor(ConsoleColor.White);
            WriteLine("] " + message);
            Thread.Sleep(10000);
            System.Environment.Exit(1);
        }
    }
}
