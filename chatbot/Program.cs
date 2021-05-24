using System;
using DSharpPlus;
using System.Threading.Tasks;

namespace chatbot
{
  class Program
  {

    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {

      var discord = new DiscordClient(new DiscordConfiguration()
      {
        Token = "Nzc5OTY2OTE5NzMzNTQyOTUy.X7oOzA.MmmhqgeJykXvsk5F-eKNSZgN-9k",
        TokenType = TokenType.Bot
      });
      Console.WriteLine("==> Bot is running");
      discord.MessageCreated += async (e) =>
                 {
                   if (e.Message.Content.ToLower().StartsWith("ping"))
                     await e.Message.RespondAsync("pong!");
                   // checks wether the user who sent the message is a bot


                   if (e.Message.Content.ToLower().StartsWith("!help"))
                   {
                     var resp = "`!help -- brings you to this\n!analyze {topic} -- will search twitter and return the general sentiment on that topic\n!user @{username} -- will analyze tweets a user has posted and return a sentiment about them\n!s {sentence} -- will analyze the sentence passed in`";
                     await e.Message.RespondAsync($"{resp}");
                   }

                 };

      await discord.ConnectAsync();
      await Task.Delay(-1);
    }

  }
}
