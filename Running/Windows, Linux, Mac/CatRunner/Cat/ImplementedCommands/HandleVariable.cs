using CatRunner.Cat.Math;
using CatRunner.Cat.Variables;
using System;
using System.Collections.Generic;
using System.IO;

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
                        if (!executor.CurrentVariables.ContainsKey(originalVarName))
                        {
                            throw new InvalidOperationException($"Variable '{originalVarName}' not found.");
                        }

                        object originalVarValue = executor.CurrentVariables[originalVarName].Value;
                        Mathlib mathlib = new Mathlib();

                        switch (math)
                        {
                            case "+=":
                                {
                                    executor.CurrentVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '+', modifiedVar }, executor.CurrentVariables[originalVarName].Type);
                                }
                                break;
                            case "-=":
                                {
                                    executor.CurrentVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '-', modifiedVar }, executor.CurrentVariables[originalVarName].Type);
                                }
                                break;
                            case "*=":
                                {
                                    executor.CurrentVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '*', modifiedVar }, executor.CurrentVariables[originalVarName].Type);
                                }
                                break;
                            case "/=":
                                {
                                    executor.CurrentVariables[originalVarName].Value = mathlib.Evaluate(new List<object> { originalVarValue, '/', modifiedVar }, executor.CurrentVariables[originalVarName].Type);
                                }
                                break;
                            case "=":
                                {
                                    executor.CurrentVariables[originalVarName].Value = modifiedVar;
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
