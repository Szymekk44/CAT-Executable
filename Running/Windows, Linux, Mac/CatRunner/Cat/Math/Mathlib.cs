using CatRunner.Cat.Variables;

namespace CatRunner.Cat.Math
{
	public class Mathlib
	{
		public object Evaluate(List<object> operations, VarType type)
		{
			switch (type)
			{
				case VarType.String:
					return CalculateString(operations);
				case VarType.Bool:
					var rpnBool = ConvertToRPN(operations, VarType.Bool);
					return EvaluateRPN(rpnBool, VarType.Bool);
				case VarType.Byte:
					var rpnByte = ConvertToRPN(operations, VarType.Byte);
					return EvaluateRPN(rpnByte, VarType.Byte);
				case VarType.Short:
					var rpnShort = ConvertToRPN(operations, VarType.Short);
					return EvaluateRPN(rpnShort, VarType.Short);
				case VarType.UShort:
					var rpnUShort = ConvertToRPN(operations, VarType.UShort);
					return EvaluateRPN(rpnUShort, VarType.UShort);
				case VarType.Int:
					var rpnInt = ConvertToRPN(operations, VarType.Int);
					return EvaluateRPN(rpnInt, VarType.Int);
				case VarType.UInt:
					var rpnUInt = ConvertToRPN(operations, VarType.UInt);
					return EvaluateRPN(rpnUInt, VarType.UInt);
				case VarType.Long:
					var rpnLong = ConvertToRPN(operations, VarType.Long);
					return EvaluateRPN(rpnLong, VarType.Long);
				case VarType.ULong:
					var rpnULong = ConvertToRPN(operations, VarType.ULong);
					return EvaluateRPN(rpnULong, VarType.ULong);
				case VarType.Float:
					var rpnFloat = ConvertToRPN(operations, VarType.Float);
					return EvaluateRPN(rpnFloat, VarType.Float);
				case VarType.Double:
					var rpnDouble = ConvertToRPN(operations, VarType.Double);
					return EvaluateRPN(rpnDouble, VarType.Double);
				default:
					Console.WriteLine("Error: unknwon Evaluate type: " + type);
					return 404;
			}
		}
		private object CalculateString(List<object> operations)
		{
			string toReturn = "";
			for (int i = 0; i < operations.Count; i++)
			{
				if (operations[i] is string)
				{
					toReturn += operations[i];
				}
				else if (operations[i] is char)
				{

				}
			}
			return toReturn;
		}

		private List<object> ConvertToRPN(List<object> tokens, VarType type)
		{
			var output = new List<object>();
			var operators = new Stack<char>();
			var precedence = new Dictionary<char, int>
	{
		{ '+', 1 },
		{ '-', 1 },
		{ '*', 2 },
		{ '/', 2 },
		{ '%', 2 },
		{ '(', 0 },
		{ ')', 0 }
	};

			foreach (var token in tokens)
			{
				if (token is double || token is float || token is ulong || token is long || token is uint || token is int || token is ushort || token is short || token is byte) // ADD HERE IF STACK IS EMPTY!
				{
					output.Add(token);
				}
				else if (token is char)
				{
					char c = (char)token;

					if (c == '(')
					{
						operators.Push(c);
					}
					else if (c == ')')
					{
						while (operators.Peek() != '(')
						{
							output.Add(operators.Pop());
						}
						operators.Pop();
					}
					else
					{
						while (operators.Any() && precedence[operators.Peek()] >= precedence[c])
						{
							output.Add(operators.Pop());
						}
						operators.Push(c);
					}
				}
			}

			while (operators.Any())
			{
				output.Add(operators.Pop());
			}

			return output;
		}


		private object EvaluateRPN(List<object> rpn, VarType type)
		{
			var stack = new Stack<object>();

			foreach (var token in rpn)
			{
				if (token is double || token is float || token is ulong || token is long || token is uint || token is int || token is ushort || token is short || token is byte)
				{
					stack.Push(token);
				}
				else if (token is char)
				{
					object right = stack.Pop();
					object left = stack.Pop();
					char op = (char)token;

					switch (type)
					{
						case VarType.Byte:
							stack.Push(EvaluateOperation(Convert.ToByte(left), Convert.ToByte(right), op));
							break;
						case VarType.Short:
							stack.Push(EvaluateOperation(Convert.ToInt16(left), Convert.ToInt16(right), op));
							break;
						case VarType.UShort:
							stack.Push(EvaluateOperation(Convert.ToUInt16(left), Convert.ToUInt16(right), op));
							break;
						case VarType.Int:
							stack.Push(EvaluateOperation(Convert.ToInt32(left), Convert.ToInt32(right), op));
							break;
						case VarType.UInt:
							stack.Push(EvaluateOperation(Convert.ToUInt32(left), Convert.ToUInt32(right), op));
							break;
						case VarType.Long:
							stack.Push(EvaluateOperation(Convert.ToInt64(left), Convert.ToInt64(right), op));
							break;
						case VarType.ULong:
							stack.Push(EvaluateOperation(Convert.ToUInt64(left), Convert.ToUInt64(right), op));
							break;
						case VarType.Float:
							stack.Push(EvaluateOperation(Convert.ToSingle(left), Convert.ToSingle(right), op));
							break;
						case VarType.Double:
							stack.Push(EvaluateOperation(Convert.ToDouble(left), Convert.ToDouble(right), op));
							break;
						default:
							throw new Exception("Unsupported variable type");
					}
				}
				else
				{
					Console.WriteLine("Unsupported type: " + token.GetType());
				}
			}

			try
			{
				object result = stack.Pop();

				// Convert result to the appropriate type
				switch (type)
				{
					case VarType.Bool:
						return Convert.ToBoolean(result);
					case VarType.Byte:
						return Convert.ToByte(result);
					case VarType.Short:
						return Convert.ToInt16(result);
					case VarType.UShort:
						return Convert.ToUInt16(result);
					case VarType.Int:
						return Convert.ToInt32(result);
					case VarType.UInt:
						return Convert.ToUInt32(result);
					case VarType.Long:
						return Convert.ToInt64(result);
					case VarType.ULong:
						return Convert.ToUInt64(result);
					case VarType.Float:
						return Convert.ToSingle(result);
					case VarType.Double:
						return Convert.ToDouble(result);
					default:
						throw new Exception("Unsupported variable type");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
				return null;
			}
		}


		private object EvaluateOperation(short left, short right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: throw new Exception("Unsupported operation");
			}
		}

		private object EvaluateOperation(ushort left, ushort right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: throw new Exception("Unsupported operation");
			}
		}

		private object EvaluateOperation(int left, int right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: return left;
			}
		}

		private object EvaluateOperation(uint left, uint right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: throw new Exception("Unsupported operation");
			}
		}

		private object EvaluateOperation(long left, long right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: throw new Exception("Unsupported operation");
			}
		}

		private object EvaluateOperation(ulong left, ulong right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: throw new Exception("Unsupported operation");
			}
		}

		private object EvaluateOperation(float left, float right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: throw new Exception("Unsupported operation");
			}
		}

		private object EvaluateOperation(double left, double right, char op)
		{
			switch (op)
			{
				case '+': return left + right;
				case '-': return left - right;
				case '*': return left * right;
				case '/': return left / right;
				case '%': return left % right;
				default: throw new Exception("Unsupported operation");
			}
		}

	}
}
