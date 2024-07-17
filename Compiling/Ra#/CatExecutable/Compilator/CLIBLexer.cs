using System.Text;

namespace CatExecutable.Compilator
{
	public enum clibTokenType
	{
		IDENT, //commands and other
		STRING, //""
		INT, //numbers
		STARTPAREN, //(
		ENDPAREN, //)
		COLON, //:
		DOT, //.
		SEMI, //;
		COMMA, //,
		STARTCLLFUNCTION,
		ENDCLLFUNCTION,
		MATHS // + - * / % etc
	}
	public class clibToken
	{
		public clibTokenType Type;
		public string? Value = null;
	}
	public class clibLexer
	{
		private string Data;
		private StringBuilder Buffer;
		private int Index;

		public clibLexer(string Data)
		{
			Buffer = new StringBuilder();
			Index = 0;
			this.Data = Data;
		}
		public clibToken[] Lex()
		{
			List<clibToken> Tokens = new();
			StringBuilder Buffer = new(); // Ensure Buffer is initialized

			while (Read().HasValue)
			{
				char? currentChar = Read();

				switch (currentChar)
				{
					case '>':
						{
							if (Read().HasValue && Read() == '>')
							{
								Eat();
								Tokens.Add(new clibToken
								{
									Type = clibTokenType.STARTCLLFUNCTION,
									Value = ">>"
								});
								Eat();
							}
						}
						break;
					case '<':
						{
							if (Read().HasValue && Read() == '<')
							{
								Eat();
								Tokens.Add(new clibToken
								{
									Type = clibTokenType.ENDCLLFUNCTION,
									Value = "<<"
								});
								Eat();
							}
						}
						break;
					case '(':
						Tokens.Add(new clibToken
						{
							Type = clibTokenType.STARTPAREN,
							Value = Eat().ToString()
						});
						break;
					case ')':
						Tokens.Add(new clibToken
						{
							Type = clibTokenType.ENDPAREN,
							Value = Eat().ToString()
						});
						break;
					case ':':
						Tokens.Add(new clibToken
						{
							Type = clibTokenType.COLON,
							Value = Eat().ToString()
						});
						break;
					case ';':
						Tokens.Add(new clibToken
						{
							Type = clibTokenType.SEMI,
							Value = Eat().ToString()
						});
						break;
					case ',':
						Tokens.Add(new clibToken
						{
							Type = clibTokenType.COMMA,
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
						Tokens.Add(new clibToken
						{
							Type = clibTokenType.STRING,
							Value = Buffer.ToString()
						});
						Buffer.Clear();
						Eat(); // Eat closing quote
						break;
					case '+':
						{
							char mathChar = Eat();
							if (Read().HasValue && (Read() == '=' || Read() == '+'))
							{
								Tokens.Add(new clibToken
								{
									Type = clibTokenType.MATHS,
									Value = mathChar.ToString() + Eat().ToString()
								});
							}
							else
							{
								Tokens.Add(new clibToken
								{
									Type = clibTokenType.MATHS,
									Value = mathChar.ToString()
								});
							}
							break;
						}
					case '-':
						Eat();
						if (Read().HasValue && (Read() == '=' || Read() == '-'))
						{
							Tokens.Add(new clibToken
							{
								Type = clibTokenType.MATHS,
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
							Tokens.Add(new clibToken
							{
								Type = clibTokenType.INT,
								Value = Buffer.ToString()
							});
							Buffer.Clear();
						}
						else
						{
							Tokens.Add(new clibToken
							{
								Type = clibTokenType.MATHS,
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
								Tokens.Add(new clibToken
								{
									Type = clibTokenType.IDENT,
									Value = ident.Substring(0, ident.Length - 2)
								});
								Tokens.Add(new clibToken
								{
									Type = clibTokenType.MATHS,
									Value = ident.Substring(ident.Length - 2)
								});
							}
							else
							{
								Tokens.Add(new clibToken
								{
									Type = clibTokenType.IDENT,
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
							Tokens.Add(new clibToken
							{
								Type = clibTokenType.INT,
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
