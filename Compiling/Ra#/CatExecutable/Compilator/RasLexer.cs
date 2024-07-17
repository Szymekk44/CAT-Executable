using System.Text;

namespace CatExecutable.Compilator
{
	public enum rasTokenType
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
	public class rasToken
	{
		public rasTokenType Type;
		public string? Value = null;
	}
	public class RasLexer
	{
		private string Data;
		private StringBuilder Buffer;
		private int Index;

		public RasLexer(string Data)
		{
			Buffer = new StringBuilder();
			Index = 0;
			this.Data = Data;
		}
		public rasToken[] Lex()
		{
			List<rasToken> Tokens = new();

			while (Read().HasValue)
			{
				char? currentChar = Read();

				switch (currentChar)
				{
					case '(':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.STARTPAREN,
							Value = Eat().ToString()
						});
						break;
					case ')':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.ENDPAREN,
							Value = Eat().ToString()
						});
						break;
					case '{':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.STARTBRACKET,
							Value = Eat().ToString()
						});
						break;
					case '}':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.ENDBRACKET,
							Value = Eat().ToString()
						});
						break;
					case ':':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.COLON,
							Value = Eat().ToString()
						});
						break;
					case ';':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.SEMI,
							Value = Eat().ToString()
						});
						break;
					case ',':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.COMMA,
							Value = Eat().ToString()
						});
						break;
					case '.':
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.DOT,
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
						Tokens.Add(new rasToken
						{
							Type = rasTokenType.STRING,
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
								Tokens.Add(new rasToken
								{
									Type = rasTokenType.MATHS,
									Value = mathChar.ToString() + Eat().ToString()
								});
							}
							else if (Read().HasValue && Read() == mathChar)
							{
								Tokens.Add(new rasToken
								{
									Type = rasTokenType.MATHS,
									Value = mathChar.ToString() + Eat().ToString()
								});
								Eat(); // Consume the second operator
							}
							else
							{
								Tokens.Add(new rasToken
								{
									Type = rasTokenType.MATHS,
									Value = mathChar.ToString()
								});
							}
							break;
						}
					case '-':
						Eat();
						if (Read().HasValue && (Read() == '=' || Read() == '-'))
						{
							Tokens.Add(new rasToken
							{
								Type = rasTokenType.MATHS,
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
							Tokens.Add(new rasToken
							{
								Type = rasTokenType.INT,
								Value = Buffer.ToString()
							});
							Buffer.Clear();
						}
						else
						{
							Tokens.Add(new rasToken
							{
								Type = rasTokenType.MATHS,
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
								Tokens.Add(new rasToken
								{
									Type = rasTokenType.IDENT,
									Value = ident.Substring(0, ident.Length - 2)
								});
								Tokens.Add(new rasToken
								{
									Type = rasTokenType.MATHS,
									Value = ident.Substring(ident.Length - 2)
								});
							}
							else
							{
								Tokens.Add(new rasToken
								{
									Type = rasTokenType.IDENT,
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
							Tokens.Add(new rasToken
							{
								Type = rasTokenType.INT,
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

