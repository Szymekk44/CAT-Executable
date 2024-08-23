using CatRunner.Cat.Math;
using CatRunner.Cat.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatRunner.Cat.ImplementedCommands
{
    public static class HandleVariable
    {
        public static void Handle(BinaryReader reader, Executor executor)
        {
            byte operation = reader.ReadByte();
            switch (operation)
            {
                case 0:
                    {
                        string originalVarName = reader.ReadString();
                        string math = ReadMath.Get(reader);
                        object modifiedVar = ReadVariable.Read(reader, executor);
                        Console.WriteLine("modifed var: " + modifiedVar);
                        // Sprawdzenie, czy zmienna istnieje w GlobalVariables
                        if (!executor.GlobalVariables.ContainsKey(originalVarName))
                        {
                            throw new InvalidOperationException($"Variable '{originalVarName}' not found.");
                        }

                        // Pobranie oryginalnej wartości zmiennej
                        object originalVarValue = executor.GlobalVariables[originalVarName].Value;

                        switch (math)
                        {
                            case "+=":
                                {
                                    if (originalVarValue is int && modifiedVar is int)
                                    {
                                        executor.GlobalVariables[originalVarName].Value = (int)originalVarValue + (int)modifiedVar;
                                    }
                                    else if (originalVarValue is double && modifiedVar is double)
                                    {
                                        executor.GlobalVariables[originalVarName].Value = (double)originalVarValue + (double)modifiedVar;
                                    }
                                    else if (originalVarValue is long && modifiedVar is long)
                                    {
                                        executor.GlobalVariables[originalVarName].Value = (long)originalVarValue + (long)modifiedVar;
                                    }
                                    else if (originalVarValue is string && modifiedVar is string)
                                    {
                                        executor.GlobalVariables[originalVarName].Value = (string)originalVarValue + (string)modifiedVar;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException($"Incompatible types for += operation. " +
                                            $"Original value type: {originalVarValue.GetType().Name}, " +
                                            $"Modified value type: {modifiedVar.GetType().Name}");
                                    }
                                }
                                break;


                            default:
                                throw new InvalidOperationException($"Unsupported operation '{math}'.");
                        }
                    }
                    break;
            }
        }
    }
}
