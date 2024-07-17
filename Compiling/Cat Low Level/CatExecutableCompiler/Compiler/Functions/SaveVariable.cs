using CatExecutableCompiler.Compiler.ByteFunctions;
using CatExecutableCompiler.Compiler.Lexer;

namespace CatExecutableCompiler.Compiler.Functions
{
	public static class SaveVariable
	{
		public static void Save(List<CLLToken> tokens, ref int i, VarType VariableType)
		{
			long startintBytePos = CompileToBytes.writer.BaseStream.Position;
			Write.Int(9999);
			int length = 0;
			for (int j = i; j < tokens.Count; j++)
			{
				switch (tokens[j].Type)
				{
					case CLLTokenType.MATHS: // Save math (+, -, *, ++, = etc)
						SaveMath.Save(tokens[j].Value);
						break;
					case CLLTokenType.IDENT: // Save variable
						if (CLLCompiler.CurrentVariables.ContainsKey(tokens[j].Value))
						{
							CompileToBytes.writer.Write((byte)22);
							CompileToBytes.writer.Write(tokens[j].Value);
						}
						else
							throw new Exception($"Variable {tokens[j].Value} does not exist in the current context");
						break;
					case CLLTokenType.STRING:
						{
							CompileToBytes.writer.Write((byte)23);
							CompileToBytes.writer.Write((byte)21);
							CompileToBytes.writer.Write(tokens[j].Value);
						}
						break;
					case CLLTokenType.INT:
						{
							CompileToBytes.writer.Write((byte)23);
							CompileToBytes.writer.Write((byte)15);
							CompileToBytes.writer.Write(tokens[j].Value);
						}
						break;
					case CLLTokenType.COMMA:
						{
							return;
						}
				}
				length++;
			}
			CompileToBytes.writer.BaseStream.Position = startintBytePos;
			Write.Int(length);
			CompileToBytes.writer.BaseStream.Position = CompileToBytes.writer.BaseStream.Length;
		}
		public static VarType SaveVariableCreation(List<CLLToken> tokens, ref int i, string type)
		{
			VarType varType = new VarType();
			Write.Byte(3);
			Write.Byte(2);


			switch (type)
			{
				case "string":
					varType = VarType.String; Write.Byte(21);
					break;
				case "bool":
					varType = VarType.Bool; Write.Byte(11);
					break;
				case "byte":
					varType = VarType.Byte; Write.Byte(12);
					break;
				case "short":
					varType = VarType.Short; Write.Byte(13);
					break;
				case "ushort":
					varType = VarType.Ushort; Write.Byte(14);
					break;
				case "int":
					varType = VarType.Int; Write.Byte(15);
					break;
				case "uint":
					varType = VarType.Uint; Write.Byte(16);
					break;
				case "long":
					varType = VarType.Long; Write.Byte(17);
					break;
				case "ulong":
					varType = VarType.Ulong; Write.Byte(18);
					break;
				case "float":
					varType = VarType.Float; Write.Byte(19);
					break;
				case "double":
					varType = VarType.Double; Write.Byte(20);
					break;
			}
			Write.String(tokens[i].Value);
			Console.WriteLine("Added variable: " + tokens[i].Value);
			CLLCompiler.CurrentVariables.Add(tokens[i].Value, new Variable { Type = varType });
			i++;
			return varType;
		}
	}
}
