using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatRunner.Cat.Math
{
    public static class ReadMath
    {
        public static string Get(BinaryReader reader)
        {
            byte operation = reader.ReadByte();

            switch (operation)
            {
                case 5:
                    return "=";
                case 6:
                    return "+";
                case 7:
                    return "-";
                case 8:
                    return "*";
                case 9:
                    return "/";
                case 10:
                    return "%";
                case 24:
                    {
                        byte first = reader.ReadByte();
                        byte second = reader.ReadByte();

                        if (first == 6 && second == 6)
                            return "++";
                        if (first == 7 && second == 7)
                            return "--";
                        if (first == 6 && second == 5)
                            return "+=";
                        if (first == 7 && second == 5)
                            return "-=";
                        if (first == 8 && second == 5)
                            return "*=";
                        if (first == 9 && second == 5)
                            return "/=";
                        if (first == 10 && second == 5)
                            return "%=";

                        throw new InvalidOperationException("Unknown combination of operations.");
                    }
                default:
                    throw new InvalidOperationException("Unknown operation code: " + operation);
            }
        }

    }
}
