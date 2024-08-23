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
                        if (!executor.GlobalVariables.ContainsKey(originalVarName))
                        {
                            throw new InvalidOperationException($"Variable '{originalVarName}' not found.");
                        }

                        object originalVarValue = executor.GlobalVariables[originalVarName].Value;
                        Mathlib mathlib = new Mathlib();

                        switch (math)
                        {
                            case "+=":
                                {
                                    executor.GlobalVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '+', modifiedVar }, executor.GlobalVariables[originalVarName].Type);
                                }
                                break;
                            case "-=":
                                {
                                    executor.GlobalVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '-', modifiedVar }, executor.GlobalVariables[originalVarName].Type);
                                }
                                break;
                            case "*=":
                                {
                                    executor.GlobalVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '*', modifiedVar }, executor.GlobalVariables[originalVarName].Type);
                                }
                                break;
                            case "/=":
                                {
                                    executor.GlobalVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '/', modifiedVar }, executor.GlobalVariables[originalVarName].Type);
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
