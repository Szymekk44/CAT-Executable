using CatRunner.Cat.Math;
namespace CatRunner.Cat.Variables
{
    public static class ReadVariable
    {
        public static object Read(BinaryReader reader, Executor executor)
        {
            int length = reader.ReadInt32();
            List<Object> operations = new List<Object>();
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
                                //To do: implement loading all types
                                case 15:
                                    operations[operations.Count - 1] = reader.ReadInt32();
                                    VariableType = VarType.Int;
                                    break;
                                case 17:
                                    operations[operations.Count - 1] = reader.ReadInt64();
                                    VariableType = VarType.Long;
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
                            Variable foundVariable = executor.GlobalVariables[varName];
                            operations[operations.Count - 1] = foundVariable.Value;
                            VariableType = foundVariable.Type;
                        }
                        break;
                    case 32:
                        {
                            byte type = reader.ReadByte();
                            string varName = reader.ReadString();
                            Variable foundVariable = executor.GlobalVariables[varName];

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
            }
        }
    }
}
