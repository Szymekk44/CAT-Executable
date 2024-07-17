namespace CatExecutableCompiler.Compiler
{
	public static class GetCommands
	{
		public static List<CLLCommand> Get()
		{
			List<CLLCommand> com = new List<CLLCommand>();
			for (int i = 0; i < CLLCompiler.CLLTokens!.Length;)
			{
				switch (CLLCompiler.CLLTokens[i].Type)
				{
					case Lexer.CLLTokenType.IDENT:
						CLLCommand command = new CLLCommand();
						command.value = CLLCompiler.CLLTokens![i].Value;
						i++;
						bool StillContinue = true;
						int CurrentParen = 0;
						while (StillContinue)
						{
							switch (CLLCompiler.CLLTokens[i].Type)
							{
								case Lexer.CLLTokenType.STARTPAREN:
									CurrentParen++;
									if (CurrentParen > 1)
									{
										command.tokens!.Add(CLLCompiler.CLLTokens[i]);
									}
									break;
								case Lexer.CLLTokenType.ENDPAREN:
									CurrentParen--;
									if (CurrentParen > 0)
									{
										command.tokens!.Add(CLLCompiler.CLLTokens[i]);
									}
									else
									{
										StillContinue = false;
									}
									break;
								case Lexer.CLLTokenType.SEMI:
									{
										StillContinue = false;
									}
									break;
								default:
									command.tokens!.Add(CLLCompiler.CLLTokens[i]);
									break;
							}
							i++;
						}
						com.Add(command);
						break;
					case Lexer.CLLTokenType.SEMI:
						i++;
						break;
					default:
						com.Add(new CLLCommand { value = CLLCompiler.CLLTokens[i].Value });
						i++;
						break;
				}
			}
			return com;
		}
	}
}
