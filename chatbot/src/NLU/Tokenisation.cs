using System;
using System.Collections.Generic;

namespace Tokenisation
{
  // defines a token instance
  public struct Token
  {
    public string token { get; set; }
    public string entity { get; set; } // this is the type of thing the word is such as Location, Date etc..
  }

  // stores the tokens of the message the bot can use
  public struct Message
  {
    public List<Token> tokens { get; set; }
    public string originalMessage { get; set; }
    public Message(string og)
    {
      this.tokens = new List<Token>();
      this.originalMessage = og;
    }
  }

  // this class handles splitting the input string up into tokens
  public class Tokeniser
  {
    public Message tokenise(string input)
    {
      Message message = new Message();
      message.originalMessage = input;

      return message;

    }
  }
}