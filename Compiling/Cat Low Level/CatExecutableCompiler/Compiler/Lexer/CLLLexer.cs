using System.Text;

namespace CatExecutableCompiler.Compiler.Lexer
{
	public enum CLLTokenType
	{
		IDENT, //commands and other
		STRING, //""
		INT, //numbers
		STARTPAREN, //(
		ENDPAREN, //)
		STARTBRACKET,
		ENDBRACKET,
		COLON, //:
		DOT, //.
		SEMI, //;
		COMMA, //,
		MATHS // + - * / % etc
	}
	public class CLLToken
	{
		public CLLTokenType Type;
		public string Value = "";
	}
	public class CLLLexer
	{
		private string Data;
		private StringBuilder Buffer;
		private int Index;

		public CLLLexer(string Data)
		{
			Buffer = new StringBuilder();
			Index = 0;
			this.Data = Data;
		}
		public CLLToken[] Lex()
		{
			List<CLLToken> Tokens = new();

			while (Read().HasValue)
			{
				char? currentChar = Read();

				switch (currentChar)
				{
					case '(':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.STARTPAREN,
							Value = Eat().ToString()
						});
						break;
					case ')':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.ENDPAREN,
							Value = Eat().ToString()
						});
						break;
					case '{':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.STARTBRACKET,
							Value = Eat().ToString()
						});
						break;
					case '}':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.ENDBRACKET,
							Value = Eat().ToString()
						});
						break;
					case ':':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.COLON,
							Value = Eat().ToString()
						});
						break;
					case ';':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.SEMI,
							Value = Eat().ToString()
						});
						break;
					case ',':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.COMMA,
							Value = Eat().ToString()
						});
						break;
					case '.':
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.DOT,
							Value = Eat().ToString()
						});
						break;
					case '"':
						Eat();
						while (Read().HasValue && Read()!.Value != '"')
						{
							Buffer.Append(Eat());
						}
						if (!Read().HasValue)
						{
							throw new Exception("String was not closed! String: " + Buffer);
						}
						Tokens.Add(new CLLToken
						{
							Type = CLLTokenType.STRING,
							Value = Buffer.ToString()
						});
						Buffer.Clear();
						Eat(); // Eat closing quote
						break;
					case '!':
					case '<':
					case '>':
					case '=':
					case '+':
					case '*':
					case '/':
					case '%':

						{
							char mathChar = Eat();
							if (Read().HasValue && Read() == '=')
							{
								Tokens.Add(new CLLToken
								{
									Type = CLLTokenType.MATHS,
									Value = mathChar.ToString() + Eat().ToString()
								});
							}
							else if (Read().HasValue && Read() == mathChar)
							{
								Tokens.Add(new CLLToken
								{
									Type = CLLTokenType.MATHS,
									Value = mathChar.ToString() + Eat().ToString()
								});
								Eat(); // Consume the second operator
							}
							else
							{
								Tokens.Add(new CLLToken
								{
									Type = CLLTokenType.MATHS,
									Value = mathChar.ToString()
								});
							}
							break;
						}
					case '-':
						Eat();
						if (Read().HasValue && (Read() == '=' || Read() == '-'))
						{
							Tokens.Add(new CLLToken
							{
								Type = CLLTokenType.MATHS,
								Value = "-" + Eat().ToString()
							});
						}
						else if (Read().HasValue && char.IsDigit(Read()!.Value))
						{
							Buffer.Append('-');
							while (Read().HasValue && char.IsDigit(Read()!.Value))
							{
								Buffer.Append(Eat());
							}
							Tokens.Add(new CLLToken
							{
								Type = CLLTokenType.INT,
								Value = Buffer.ToString()
							});
							Buffer.Clear();
						}
						else
						{
							Tokens.Add(new CLLToken
							{
								Type = CLLTokenType.MATHS,
								Value = '-'.ToString()
							});
						}
						break;
					default:
						if (char.IsLetter(Read()!.Value))
						{
							while (Read().HasValue && char.IsLetterOrDigit(Read()!.Value))
							{
								Buffer.Append(Eat());
							}
							string ident = Buffer.ToString();
							if (ident.EndsWith("++") || ident.EndsWith("--"))
							{
								Tokens.Add(new CLLToken
								{
									Type = CLLTokenType.IDENT,
									Value = ident.Substring(0, ident.Length - 2)
								});
								Tokens.Add(new CLLToken
								{
									Type = CLLTokenType.MATHS,
									Value = ident.Substring(ident.Length - 2)
								});
							}
							else
							{
								Tokens.Add(new CLLToken
								{
									Type = CLLTokenType.IDENT,
									Value = ident
								});
							}
							Buffer.Clear();
						}
						else if (char.IsWhiteSpace(Read()!.Value))
						{
							Eat();
						}
						else if (char.IsDigit(Read()!.Value))
						{
							while (Read().HasValue && char.IsDigit(Read()!.Value))
							{
								Buffer.Append(Eat());
							}
							Tokens.Add(new CLLToken
							{
								Type = CLLTokenType.INT,
								Value = Buffer.ToString()
							});
							Buffer.Clear();
						}
						else
						{
							Eat();
						}
						break;
				}
			}
			return Tokens.ToArray();
		}
		private char Eat()
		{
			Index++;
			return Data[Index - 1];
		}
		private char? Read(int Offset = 0)
		{
			if (Offset + Index >= Data.Length)
			{
				return null;
			}
			return Data[Offset + Index];
		}
	}
}
