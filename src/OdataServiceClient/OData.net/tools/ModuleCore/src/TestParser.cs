//---------------------------------------------------------------------
// <copyright file="TestParser.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Text;              //StringBuilder
using System.Collections;		//HashTable

namespace Microsoft.Test.ModuleCore
{
	////////////////////////////////////////////////////////////////
	// KeywordParser
	//
	////////////////////////////////////////////////////////////////
	public class KeywordParser
	{ 
		//Enum
		protected enum PARSE
		{
			Initial,
			Keyword,
			Equal,
			Value,
			SingleBegin,
			SingleEnd,
			DoubleBegin,
			DoubleEnd,
			End
		};

		//Accessors
		//Note: You can override these if you want to leverage our parser (inherit), and change
		//some of the behavior (without reimplementing it).
		static Tokens	DefaultTokens	= new Tokens();
		public class	Tokens
		{
			public	string	Equal		= "=";
			public	string	Seperator	= ";";
			public	string	SingleQuote	= "'";
			public	string	DoubleQuote	= "\"";
		};
		

		//Methods
		static public	Hashtable		ParseKeywords(string str)
		{
			return ParseKeywords(str, DefaultTokens);
		}
		
		static public 	Hashtable		ParseKeywords(string str, Tokens tokens)
		{
			PARSE state			            = PARSE.Initial;
			int index			            = 0;
			int keyStart		            = 0;
			InsensitiveHashtable keywords  = new InsensitiveHashtable(5);
			string key			            = null;

			if(str != null)
			{
				StringBuilder builder = new StringBuilder(str.Length);
				for(; index < str.Length; index ++)
				{
					char ch = str[index];
					switch(state)
					{
						case PARSE.Initial:
							if(Char.IsLetterOrDigit(ch))
							{
								keyStart = index;
								state    = PARSE.Keyword;
							}
							break;

						case PARSE.Keyword:
                            if(tokens.Seperator.IndexOf(ch) >= 0)
                            {
                                state = PARSE.Initial;
                            }
                            else if(tokens.Equal.IndexOf(ch) >= 0)
							{
								//Note: We have a case-insentive hashtable so we don't have to lowercase
								key = str.Substring(keyStart, index - keyStart).Trim();
								state = PARSE.Equal;
 							}
							break;

						case PARSE.Equal:
							if(Char.IsWhiteSpace(ch))
								break;
							builder.Length = 0;
							
							//Note: Since we allow you to alter the tokens, these are not 
							//constant values, so we cannot use a switch statement...
							if(tokens.SingleQuote.IndexOf(ch) >= 0)
							{
								state = PARSE.SingleBegin;
							}
							else if(tokens.DoubleQuote.IndexOf(ch) >= 0)
							{
								state = PARSE.DoubleBegin;
							}
							else if(tokens.Seperator.IndexOf(ch) >= 0)
							{
								keywords.Update(key, String.Empty);
								state = PARSE.Initial;
							}
							else
							{
								state = PARSE.Value;
								builder.Append(ch);
							}
							break;

						case PARSE.Value:
							if(tokens.Seperator.IndexOf(ch) >= 0)
							{
								keywords.Update(key, (builder.ToString()).Trim());
								state = PARSE.Initial;
							}
							else
							{
								builder.Append(ch);
							}
							break;

						case PARSE.SingleBegin:
							if(tokens.SingleQuote.IndexOf(ch) >= 0)
								state = PARSE.SingleEnd;
							else
								builder.Append(ch);
							break;

						case PARSE.DoubleBegin:
							if(tokens.DoubleQuote.IndexOf(ch) >= 0)
								state = PARSE.DoubleEnd;
							else
								builder.Append(ch);
							break;

						case PARSE.SingleEnd:
							if(tokens.SingleQuote.IndexOf(ch) >= 0)
							{
								state = PARSE.SingleBegin;
								builder.Append(ch);
							}
							else
							{
								keywords.Update(key, builder.ToString());
								state = PARSE.End;
								goto case PARSE.End;
							}
							break;

						case PARSE.DoubleEnd:
							if(tokens.DoubleQuote.IndexOf(ch) >= 0)
							{
								state = PARSE.DoubleBegin;
								builder.Append(ch);
							}
							else
							{
								keywords.Update(key, builder.ToString());
								state = PARSE.End;
								goto case PARSE.End;
							}
							break;

						case PARSE.End:
							if(tokens.Seperator.IndexOf(ch) >= 0)
							{
								state = PARSE.Initial;
							}
							break;

						default:
							throw new TestFailedException("Unhandled State: " + StringEx.ToString(state));
					}
				}

				switch(state)
				{
					case PARSE.Initial:
					case PARSE.Keyword:
					case PARSE.End:
						break;

					case PARSE.Equal:
						keywords.Update(key, String.Empty);
						break;
					
					case PARSE.Value:
						keywords.Update(key, (builder.ToString()).Trim());
						break;
					
					case PARSE.SingleBegin:
					case PARSE.DoubleBegin:
					case PARSE.SingleEnd:
					case PARSE.DoubleEnd:
						keywords.Update(key, builder.ToString());
						break;

					default:
						throw new TestFailedException("Unhandled State: " + StringEx.ToString(state));
				};
			}
			return keywords;
		}	
	}
}


