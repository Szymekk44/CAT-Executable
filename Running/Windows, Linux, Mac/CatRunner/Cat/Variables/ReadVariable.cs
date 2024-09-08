using CatRunner.Cat.Math;
using System.IO;
using System;
using System.Collections.Generic;
namespace CatRunner.Cat.Variables
{
    public static class ReadVariable
    {
        public static object Read(BinaryReader reader, Executor executor)
        {
            int length = reader.ReadInt32();
            List<object> operations = new List<object>();
            int paren = 0;
            VarType VariableType = VarType.Int;
            for (int i = 0; i < length; i++)
            {
                byte action = reader.ReadByte();
                operations.Add(new MathOperation());

                switch (action)
                {
                    case 23: //Not from memory (not variable). 
                        {
                            byte type = reader.ReadByte();
                            switch (type)
                            {
                                case 11:
                                    operations[operations.Count - 1] = reader.ReadBoolean();
                                    VariableType = VarType.Bool;
                                    break;
                                case 12:
                                    operations[operations.Count - 1] = reader.ReadByte();
                                    VariableType = VarType.Byte;
                                    break;
                                case 13:
                                    operations[operations.Count - 1] = reader.ReadInt16();
                                    VariableType = VarType.Short;
                                    break;
                                case 14:
                                    operations[operations.Count - 1] = reader.ReadUInt16();
                                    VariableType = VarType.UShort;
                                    break;
                                case 15:
                                    operations[operations.Count - 1] = reader.ReadInt32();
                                    VariableType = VarType.Int;
                                    break;
                                case 16:
                                    operations[operations.Count - 1] = reader.ReadUInt32();
                                    VariableType = VarType.UInt;
                                    break;
                                case 17:
                                    operations[operations.Count - 1] = reader.ReadInt64();
                                    VariableType = VarType.Long;
                                    break;
                                case 18:
                                    operations[operations.Count - 1] = reader.ReadUInt64();
                                    VariableType = VarType.ULong;
                                    break;
                                case 19:
                                    operations[operations.Count - 1] = reader.ReadSingle();
                                    VariableType = VarType.Float;
                                    break;
                                case 20:
                                    operations[operations.Count - 1] = reader.ReadDouble();
                                    VariableType = VarType.Double;
                                    break;
                                case 21:
                                    operations[operations.Count - 1] = reader.ReadString();
                                    VariableType = VarType.String;
                                    break;
                            }
                        }
                        break;
                    case 22: //Variable from memory
                        {
                            string varName = reader.ReadString();
                            Variable foundVariable = executor.CurrentVariables[varName];
                            operations[operations.Count - 1] = foundVariable.Value;
                            VariableType = foundVariable.Type;
                        }
                        break;
                    case 32:
                        {
                            byte type = reader.ReadByte();
                            string varName = reader.ReadString();
                            Variable foundVariable = executor.CurrentVariables[varName];

                            // Ręczne kopiowanie wartości i typu, aby newVariable była niezależna od foundVariable
                            Variable newVariable = new Variable
                            {
                                Value = foundVariable.Value,
                                Type = foundVariable.Type
                            };

                            switch (type)
                            {
                                case 21:
                                    {
                                        newVariable.Value = Convert.ToString(foundVariable.Value);
                                        newVariable.Type = VarType.String;
                                    }
                                    break;
                            }

                            operations[operations.Count - 1] = newVariable.Value;
                            VariableType = newVariable.Type;
                        }
                        break;
                    case 5:
                        operations[operations.Count - 1] = '=';
                        break;
                    case 6:
                        operations[operations.Count - 1] = '+';
                        break;
                    case 7:
                        operations[operations.Count - 1] = '-';
                        break;
                    case 8:
                        operations[operations.Count - 1] = '*';
                        break;
                    case 9:
                        operations[operations.Count - 1] = '/';
                        break;
                    case 10:
                        operations[operations.Count - 1] = '%';
                        break;
                    case 26:
                        operations[operations.Count - 1] = '<';
                        break;
                    case 27:
                        operations[operations.Count - 1] = '>';
                        break;
                    case 28: // start parenthesis '('
                        {
                            operations[operations.Count - 1] = '(';
                            paren++;
                        }
                        break;
                    case 29: // end parenthesis ')'
                        {
                            operations[operations.Count - 1] = ')';
                            paren--;
                        }
                        break;
                }
            }
            var evaluator = new Mathlib();
            object CurVar = evaluator.Evaluate(operations, VariableType);
            return CurVar;
        }
        public static void CreateVariable(BinaryReader reader, Executor executor, bool ReadFullHeader = false, bool Global = false)
        {
            if (ReadFullHeader)
            {
                Commands action1 = (Commands)reader.ReadByte();
                Commands action2 = (Commands)reader.ReadByte();
                if (action1 != Commands.Create || action2 != Commands.Variable)
                    throw new Exception($"Invalid variable creation: {action1} : {action2}");
            }
            byte VariableType = reader.ReadByte();
            VarType type = GetVariableType.Get(VariableType);
            string VarName = reader.ReadString();
            object? CurVar = null;
            Commands next = (Commands)reader.ReadByte();
            if (next == Commands.Continue)
            {
                string operation = ReadMath.Get(reader);
                CurVar = Read(reader, executor);
            }
            if (Global)
            {
                executor.GlobalVariables.Add(VarName, new Variable { Value = CurVar!, Type = type });
                executor.CurrentVariables.Add(VarName, new Variable { Value = CurVar!, Type = type });
            }
            else
            {
                executor.CurrentVariables.Add(VarName, new Variable { Value = CurVar!, Type = type });
                executor.LocalScopes[executor.LocalScopes.Count - 1].Add(VarName, new Variable { Value = CurVar!, Type = type });
            }
        }
    }
}
